using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetAsync(u => u.Email == email, u => u.Role);
        }

        public async Task<int> ActiveAccount(string email)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.Status = StatusUsersConstants.ACTIVE;
            return await _unitOfWork.CommitAsync();
        }
    }
}