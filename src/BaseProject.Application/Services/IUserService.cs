using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<int> ActiveAccount(string email);
    }
}