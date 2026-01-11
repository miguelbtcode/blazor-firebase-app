using Microsoft.AspNetCore.Mvc;
using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UserRegister;
using NetFirebase.Api.Services.Authentication;

namespace NetFirebase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UserController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public UserController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(
        [FromBody] UserRegisterRequestDto request,
        CancellationToken cancellationToken
    )
    {
        return await _authenticationService.RegisterAsync(request, cancellationToken);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken
    )
    {
        return await _authenticationService.LoginAsync(request, cancellationToken);
    }
}
