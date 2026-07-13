using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;


        // Transaction support
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();


        Task<int> SaveAsync();


    }
}
