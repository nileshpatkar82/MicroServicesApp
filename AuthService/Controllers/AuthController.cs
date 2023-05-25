using AuthService.Database.Entities;
using AuthService.Models;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            return _authService.GetUsers();
        }
        [HttpPost]
        public IActionResult CreateUser(SignUpModel model)
        {
            User user = new User
            {
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
            };
            var Result = _authService.CreateUser(user, model.Role);
            if (Result)
            {
                UserModel userModel = new UserModel
                {
                    Id = user.Id,
                    Email = model.Email,
                    Name = model.Name,
                    Roles = user.Roles.Select(r => r.Name).ToArray()
                };

                return CreatedAtAction("CreateUser", userModel);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public IActionResult ValidateUser(LoginModel model)
        {
            UserModel user = _authService.ValidateUser(model);
            if (user != null)
                return Ok(user);
            else
                return BadRequest();


        }
    }
}
