using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UserRegister;

namespace NetFirebase.Api.Services.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        UserRegisterRequestDto request,
        CancellationToken cancellationToken = default
    );
    Task<string> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
