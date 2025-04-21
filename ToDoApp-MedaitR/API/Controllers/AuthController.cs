using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp_MedaitR.Application.Auth.Commands;

namespace ToDoApp_MedaitR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Login endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            if (loginCommand == null || string.IsNullOrEmpty(loginCommand.UserName) || string.IsNullOrEmpty(loginCommand.Password))
            {
                return BadRequest("Invalid username or password.");
            }

            var response = await _mediator.Send(loginCommand);

            // Login başarılıysa, access token ve refresh token döndürülür
            return Ok(new
            {
                accessToken = response.AccessToken,
                refreshToken = response.RefreshToken
            });
        }

        // Register endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            if (registerCommand == null || string.IsNullOrEmpty(registerCommand.UserName) || string.IsNullOrEmpty(registerCommand.Password))
            {
                return BadRequest("Invalid username or password");
            }

            try
            {
                var response = await _mediator.Send(registerCommand);

                // Kayıt başarılıysa, access token ve refresh token döndürülür
                return Ok("User registered");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kayıt sırasında hata oluştu: {ex.Message}");
            }
        }

        // Refresh Token endpoint
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            if (refreshTokenCommand == null || string.IsNullOrEmpty(refreshTokenCommand.RefreshToken))
            {
                return BadRequest("Invalid Refresh Token.");
            }

            try
            {
                var response = await _mediator.Send(refreshTokenCommand);
                return Ok(new
                {
                    accessToken = response.AccessToken,
                    refreshToken = response.RefreshToken
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid or expired Refresh Token");
            }
        }
    }
}
