using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string emailTo, string subject, string body);
    }
}