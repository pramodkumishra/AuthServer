using System;
using System.Security.Claims;
using AuthServer.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AuthServer.AuthAPI.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }
        public string UserId { get; }
    }
}
