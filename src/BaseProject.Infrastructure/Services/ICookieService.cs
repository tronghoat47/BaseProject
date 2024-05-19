using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure.Services
{
    public interface ICookieService
    {
        void SetCookie(string key, string value, int? expirationDays, int? expirationMinutes);

        void RemoveCookie(string key);
    }
}