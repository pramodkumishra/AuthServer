using System;
using AuthServer.Application.Interfaces.Repositories;
using AuthServer.Core.Entities;
using AuthServer.Infrastructure.Data;
using AuthServer.Infrastructure.Repositories.Base;

namespace AuthServer.Infrastructure.Repositories
{
    public class MovieRepositoryAsync : GenericRepositoryAsync<Movie>, IMovieRepositoryAsync
    {
        public MovieRepositoryAsync(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
