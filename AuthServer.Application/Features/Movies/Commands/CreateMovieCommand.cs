using System;
using System.Threading;
using System.Threading.Tasks;
using AuthServer.Application.Interfaces.Repositories;
using AuthServer.Application.ResponseWrappers;
using AuthServer.Core.Entities;
using AutoMapper;
using MediatR;

namespace AuthServer.Application.Features.Movies.Commands
{
    public class CreateMovieCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public double Duration { get; set; }
    }
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Response<int>>
    {
        private readonly IMovieRepositoryAsync _movieRepositoryAsync;
        private readonly IMapper _mapper;
        public CreateMovieCommandHandler(IMovieRepositoryAsync movieRepositoryAsync, IMapper mapper)
        {
            _movieRepositoryAsync = movieRepositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<Movie>(request);
            await _movieRepositoryAsync.AddAsync(movie);
            return new Response<int>(movie.Id);
        }
    }
}
