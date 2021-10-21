using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
