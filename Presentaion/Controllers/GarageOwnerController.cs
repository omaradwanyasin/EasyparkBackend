using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Easypark_Backend.Presentaion.Controllers.UserController;

namespace Easypark_Backend.Presentation.Controllers
{
    [ApiController]
    public class GarageOwnerController : ControllerBase
    {
        private readonly UserLoggerRepo _services;

        public GarageOwnerController(UserLoggerRepo services)
        {
            _services = services;
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

            return Ok(user);
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
