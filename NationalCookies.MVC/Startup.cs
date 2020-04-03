using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NationalCookies.Data;
using NationalCookies.Data.Interfaces;
using NationalCookies.Data.Services;
using System;
using System.Threading.Tasks;

namespace NationalCookies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var useCosmos = $"{Configuration["UseCosmos"]}".ToUpper() == "TRUE";
            if (useCosmos)
            {
                var accountEndpoint = Configuration["CosmosCookie:AccountEndpoint"];
                var databaseName = Configuration["CosmosCookie:DatabaseName"];
                var authKeyName = Configuration["CosmosCookie:AccountKeyName"];
                var accountKey = Environment.GetEnvironmentVariable(authKeyName);

                services.AddDbContext<CookieContext>(options => options.UseCosmos(accountEndpoint, accountKey, databaseName));
            }
            else
            {
                services.AddDbContext<CookieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CookieDBConnection")));
            }

            var redisConnectionName = Configuration.GetConnectionString("RedisConnectionName");
            var redisConnection = Environment.GetEnvironmentVariable(redisConnectionName);

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = redisConnection;
                option.InstanceName = "master";
            });

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICookieService, CookieService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            InitializeDbContextAsync(app.ApplicationServices).Wait();
        }

        public static async Task InitializeDbContextAsync(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                using (var cookieContext = scope.ServiceProvider.GetService<CookieContext>())
                {
                    await cookieContext.EnsureCreatedAndSeedAsync();
                }
            }
        }
    }
}
