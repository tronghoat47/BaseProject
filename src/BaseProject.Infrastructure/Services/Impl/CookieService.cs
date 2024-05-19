using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure.Services.Impl
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string key, string value, int? expirationDays, int? expirationMinutes)
        {
            CookieOptions option = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            if (expirationDays.HasValue)
            {
                option.Expires = DateTime.Now.AddDays(expirationDays.Value);
            }
            if (expirationMinutes.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expirationMinutes.Value);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, option);
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
        }
    }
}