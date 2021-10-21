using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Application.Features.Movies.Commands;
using AuthServer.Application.Features.Movies.Commands.Queries;
using AuthServer.Core.Entities;
using AutoMapper;

namespace AuthServer.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<GetAllMovieViewModel, Movie>().ReverseMap();

        }

    }
}