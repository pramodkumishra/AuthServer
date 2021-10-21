using System.Globalization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.Application.Interfaces.Repositories;
using AuthServer.Core.Repositories.Base;
using AuthServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuthServer.Infrastructure.Repositories.Base
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenericRepositoryAsync(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }


        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this._applicationDbContext.Set<T>().ToListAsync();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await this._applicationDbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();

        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
