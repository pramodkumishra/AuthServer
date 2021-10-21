using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Application.Interfaces.Repositories;
using AuthServer.Application.ResponseWrappers;
using MediatR;
using System.Threading;
using AutoMapper;

namespace AuthServer.Application.Features.Movies.Commands.Queries
{
    public class GetAllMovieViewModel
    {
        public string Name { get; set; }
        public double Duration { get; set; }
    }
    public class GetAllMoviesQuery : IRequest<PagedResponse<IEnumerable<GetAllMovieViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, PagedResponse<IEnumerable<GetAllMovieViewModel>>>
    {
        private readonly IMovieRepositoryAsync _movieRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllMoviesQueryHandler(IMovieRepositoryAsync movieRepositoryAsync, IMapper mapper)
        {
            _movieRepositoryAsync = movieRepositoryAsync;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetAllMovieViewModel>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepositoryAsync.GetPagedReponseAsync(request.PageNumber, request.PageSize);
            var getAllMovieViewModel = _mapper.Map<IEnumerable<GetAllMovieViewModel>>(movies);
            return new PagedResponse<IEnumerable<GetAllMovieViewModel>>(getAllMovieViewModel, request.PageNumber, request.PageSize);

        }
    }
}