using Microsoft.EntityFrameworkCore.Storage;
using Portal.Application.IRepository;
using Portal.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly PortalDbContext _context;
        private Hashtable _repositories;
        private IDbContextTransaction _transaction;
        public UnitOfWork(PortalDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {

            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();

        }

        public void Dispose()
        {

            _transaction?.Dispose();
            _context.Dispose();

        }

        public IGenericRepository<T> Repository<T>() where T : class
        {

            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<T>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];

        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
