using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
