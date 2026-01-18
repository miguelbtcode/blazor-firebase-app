using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UserRegister;
using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Services.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        UserRegisterRequestDto request,
        CancellationToken cancellationToken = default
    );
    Task<string> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}
