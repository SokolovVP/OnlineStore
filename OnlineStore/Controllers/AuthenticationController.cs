using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contracts.Authentication;
using OnlineStore.Application.Services.Authentication;

namespace OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            AuthenticationResult result = _authenticationService.Register(
                request.FirstName, request.LastName, request.Email, request.Password);

            AuthenticationResponse response = new AuthenticationResponse(
                result.user.Id, 
                result.user.FirstName, 
                result.user.LastName,
                result.user.Email, 
                result.Token);
            
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            AuthenticationResult result = _authenticationService.Login(request.Email, request.Password);

            AuthenticationResponse response = new AuthenticationResponse(
                result.user.Id, 
                result.user.FirstName, 
                result.user.LastName, 
                result.user.Email, 
                result.Token);

            return Ok(response);
        }
    }
}
