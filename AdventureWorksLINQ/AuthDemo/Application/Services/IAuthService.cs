using AdventureWorksLINQ.AuthDemo.Application.Models;

namespace AdventureWorksLINQ.AuthDemo.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterRequest request);
        Task<AuthResult> LoginAsync(LoginRequest request);
    }
}