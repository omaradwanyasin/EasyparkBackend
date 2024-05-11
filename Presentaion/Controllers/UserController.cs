using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Easypark_Backend.Presentaion.Controllers
{
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserLoggerRepo _services;
        public UserController(UserLoggerRepo services)
        {
            _services = services;
        }
        [HttpPost]
        [Route("/signin")]

        public async Task<ActionResult<UserModels>> SignIn([FromBody] SignRequest signInRequest)
        {
            // Call the repository method to sign in the user
            var user = await _services.SignIn(signInRequest.Email, signInRequest.Password);

            if (user == null)
            {
                return NotFound("Invalid email or password");
            }

            return Ok(user);
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

