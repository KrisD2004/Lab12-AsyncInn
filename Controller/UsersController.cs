using Lab2_AysncInn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lab2_AysncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> manager)
        {

            userManager = manager;
        }

        // ROUTES

        [HttpPost("Register")]
        public async Task<ActionResult<ApplicationUser>> Register(ApplicationUser user)
        {
            // Note: data (RegisterUser) comes from an inbound DTO/Model created for this purpose
            // this.ModelState?  This comes from MVC Binding and shares an interface with the Model
            //var user = await userService.Register(data, this.ModelState);

            var result = await userManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {
                return new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = await userManager.GetRolesAsync(user)
                };
            }

            // What about our errors?
            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(user.Password)    :
                    error.Code.Contains("Email") ? nameof(user.Email) :
                    error.Code.Contains("UserName") ? nameof(user.UserName) :
                    "";
                ModelState.AddModelError(errorKey, error.Description);
            }



            if (ModelState.IsValid)
            {
                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser>> Login(ApplicationUser data)
        {

            var user = await userManager.FindByNameAsync(data.UserName);

            if (await userManager.CheckPasswordAsync(user, data.Password))
            {

                return new ApplicationUser()
                {
                    Id = user.Id,

                };
            }
            if (user == null)
            {
                return Unauthorized();
            }

            return BadRequest();
        }

        [Authorize(Policy = "create ")]
        [HttpGet("me")]
        public async Task<ApplicationUser> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }

}
