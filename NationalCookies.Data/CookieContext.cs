using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NationalCookies.Data
{
    public class CookieContext : DbContext
    {

        public CookieContext(DbContextOptions options) : base(options)
        {

        }

        public CookieContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Cookie> Cookies { get; set; }

        public async Task EnsureCreatedAndSeedAsync()
        {
            await this.Database.EnsureCreatedAsync();

            bool hasCookies = this.Cookies.Any();
            if (!hasCookies)
            {
                var cookies = new List<Cookie>()
                {
                    new Cookie() {ImageUrl="https://intcookie.azureedge.net/cdn/cookie-cc.jpg", Name = "Chololate Chip", Price = 1.2 },
                    new Cookie() {ImageUrl="https://intcookie.azureedge.net/cdn/cookie-bc.jpg", Name = "Butter Cookie", Price = 1.0 },
                    new Cookie() {ImageUrl="https://intcookie.azureedge.net/cdn/cookie-mc.jpg", Name = "Macaroons", Price = 0.9 },
                };

                this.Cookies.AddRange(cookies);
                await this.SaveChangesAsync();
            }
        }
    }
}
