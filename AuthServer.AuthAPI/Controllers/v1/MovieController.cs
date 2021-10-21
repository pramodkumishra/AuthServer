using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Application.Features.Movies.Commands;
using AuthServer.Application.Features.Movies.Commands.Queries;
using AuthServer.Application.Features.Movies.Queries;
using AuthServer.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace AuthServer.AuthAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MovieController : BaseApiController
    {

        private readonly IMovieRepositoryAsync _movieRepositoryAsync;
        public MovieController(IMovieRepositoryAsync movieRepositoryAsync)
        {
            this._movieRepositoryAsync = movieRepositoryAsync;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllMovieParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllMoviesQuery() { PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMovieCommand createMovieCommand)
        {
            return Ok(await Mediator.Send(createMovieCommand));
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}