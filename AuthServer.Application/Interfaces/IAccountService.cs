using System;
using System.Threading.Tasks;
using AuthServer.Application.DTOs.Account;
using AuthServer.Application.ResponseWrappers;

namespace AuthServer.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    }
}
