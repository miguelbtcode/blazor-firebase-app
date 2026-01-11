using FirebaseAdmin.Auth;
using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UserRegister;
using NetFirebase.Api.Models;

namespace NetFirebase.Api.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterAsync(
        UserRegisterRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var userArgs = new UserRecordArgs
        {
            DisplayName = request.FullName,
            Email = request.Email,
            Password = request.Password,
        };

        var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs, cancellationToken);

        return user.Uid;
    }

    public async Task<string> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var credentials = new
        {
            request.Email,
            request.Password,
            returnSecureToken = true,
        };

        var response = await _httpClient.PostAsJsonAsync("", credentials, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Credentials are invalid.");

        var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>(
            cancellationToken
        );

        return authFirebaseObject?.IdToken ?? string.Empty;
    }
}
