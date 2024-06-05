using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Easypark_Backend.Presentaion.Controllers
{
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserLoggerRepo _services;
        private readonly JwtOptions _jwtOptions;
        public UserController(UserLoggerRepo services, JwtOptions jwtOptions)
        {
            _services = services;
            _jwtOptions = jwtOptions;
        }
        [HttpPost]
        [Route("/signin")]
        public async Task<ActionResult> SignIn([FromBody] SignRequest signInRequest)
        {
            // Call the repository method to sign in the user
            var user = await _services.SignIn(signInRequest.Email, signInRequest.Password);

            if (user == null)
            {
                return NotFound("Invalid email or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SigningKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
        [HttpPost]
        [Route("/signup")]
        public async Task<ActionResult<UserModels>> SignUp([FromBody] SignRequest signUpRequest)
        {
            var newUser = new UserModels
            {
                Name = signUpRequest.Name,
                Email = signUpRequest.Email,
                Password = signUpRequest.Password
            };
            var createdUser = await _services.SignUp(newUser);
            return CreatedAtAction(nameof(SignUp), createdUser);

        }
        public class SignRequest
        {
            
            public string ?Name { get; set; } 
            public string Email { get; set; }
            public string Password { get; set; }
        }
        
    }

}

