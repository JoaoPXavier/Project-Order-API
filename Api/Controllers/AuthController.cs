using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrdersApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) => _config = config;

        
        // Login: returns the JWT token. Fixed credentials for testing purposes
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            // exemplo: user=admin, pass=senha
            if (dto.Username != "admin" || dto.Password != "senha") return Unauthorized();

            var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"] ?? "ReplaceThisWithARealSecretKey123!");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, dto.Username) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }

    public class LoginDto { public string Username { get; set; } = ""; public string Password { get; set; } = ""; }
}
