using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using NetFirebase.Api.Data;
using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UserRegister;
using NetFirebase.Api.Models;
using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly DatabaseContext _dbContext;

    public AuthenticationService(HttpClient httpClient, DatabaseContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
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

        _dbContext.Users.Add(
            new User
            {
                Email = request.Email,
                FullName = request.FullName,
                FirebaseId = user.Uid,
            }
        );

        await _dbContext.SaveChangesAsync(cancellationToken);

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

    public async Task<User?> GetUserByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbContext
            .Users.Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
