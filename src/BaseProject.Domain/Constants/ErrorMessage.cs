using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Constants
{
    public static class ErrorMessage
    {
        public const string ERROR_CONFIRM_PASSWORD = "ConfirmPassword must be same Password";
        public const string ERROR_PASSWORD = "Password must have at least 8 characters, at least one uppercase letter, one lowercase letter, one number and one special character";
    }
}