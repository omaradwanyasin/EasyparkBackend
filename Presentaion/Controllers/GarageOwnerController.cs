using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Easypark_Backend.Presentaion.Controllers.UserController;

namespace Easypark_Backend.Presentation.Controllers
{
    [ApiController]
    public class GarageOwnerController : ControllerBase
    {
        private readonly UserLoggerRepo _services;
        private readonly JwtOptions _jwtOptions;

        public GarageOwnerController(UserLoggerRepo services, JwtOptions jwtOptions)
        {
            _services = services;
            _jwtOptions = jwtOptions;
        }
        [HttpPost]
        [Route("/GarageOwnerSignin")]

        public async Task<ActionResult<GarageOwnerModels>> SignIngo([FromBody] SignRequest signInRequest)
        {
            // Call the repository method to sign in the user
            var user = await _services.SignInGarageOwner(signInRequest.Email, signInRequest.Password);

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

            return Ok(new { Token = tokenString});

            
        }

        [HttpPost]
        [Route("/OwnerSignUp")]
        public async Task<ActionResult<GarageOwnerModels>> SignUpGO([FromBody] SignRequestGO signUpRequest)
        {
            var newUser = new GarageOwnerModels
            {
                Name = signUpRequest.Name,
                Email = signUpRequest.Email,
                Geometry = signUpRequest.Geometry,
                Password = signUpRequest.Password,
                PhoneNumber = null,
                
            };

            var createdUser = await _services.GarageOwnerSignUp(newUser);
            return CreatedAtAction(nameof(SignUpGO), new { id = createdUser.Id }, createdUser);
        }

        public class SignRequestGO
        {
            public int GarageOwnerId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public double[] Geometry { get; set; }
            public int? PhoneNumber { get; set; }
        }
    }
}
