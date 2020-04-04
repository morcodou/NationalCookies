using Microsoft.Extensions.Caching.Distributed;
using NationalCookies.Data.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace NationalCookies.Data.Services
{
    public class CookieService : ICookieService
    {
        private CookieContext _context;
        private IDistributedCache _cache;

        const string CookiesKey = "Cookies";

        public CookieService(CookieContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public CookieService(CookieContext context) : this(context, null)
        {
        }

        public List<Cookie> GetAllCookies()
        {
            List<Cookie> cookies;

            var cachedCookies = _cache?.GetString(CookiesKey);
            if (!string.IsNullOrEmpty(cachedCookies))
            {
                cookies = JsonConvert.DeserializeObject<List<Cookie>>(cachedCookies);
            }
            else
            {
                //get the cookies from the database
                cookies = _context.Cookies.ToList();

                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new System.TimeSpan(0, 0, 15));
                _cache?.SetString(CookiesKey, JsonConvert.SerializeObject(cookies), options);
            }

            return cookies;
        }

        public void ClearCache()
        {
            _cache?.Remove(CookiesKey);
        }
    }
}
