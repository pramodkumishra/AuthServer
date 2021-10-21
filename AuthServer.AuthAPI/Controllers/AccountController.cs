using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Application.DTOs.Account;
using AuthServer.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public AccountController(IAccountService accountService, IAuthenticatedUserService authenticatedUserService)
        {
            _accountService = accountService;
            _authenticatedUserService = authenticatedUserService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(registerRequest, origin));
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.AuthenticateAsync(authenticationRequest, origin));
        }
        [HttpGet("profile")]
        [Authorize]
        public IActionResult Profile()
        {
            return Ok(_authenticatedUserService.UserId);
        }
    }
}