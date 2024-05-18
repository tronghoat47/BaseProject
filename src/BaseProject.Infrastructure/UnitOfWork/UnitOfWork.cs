using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;
using BaseProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseProjectContext _context;
        private IUserRepository _userRepository;

        public UnitOfWork(BaseProjectContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
            => _userRepository ??= new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}