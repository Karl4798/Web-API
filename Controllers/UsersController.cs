using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdMedAPI.Models;
using AdMedAPI.Repository.IRepository;

namespace AdMedAPI.Controllers
{

    [Authorize]
    [Route("api/v{version:apiVersion}/Users")]
    //[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Authenticates a user account.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {

            var user = _userRepo.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);

        }

        /// <summary>
        /// Registers a new account.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationModel model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new {message = "Username already exists"});
            }

            bool passwordsMatch = _userRepo.DoPasswordsMatch(model.Password, model.ConfirmPassword);
            if (!passwordsMatch)
            {
                return BadRequest(new { message = "Passwords do not match" });
            }

            var user = _userRepo.Register(model.Username, model.Password, model.FirstName, model.LastName);
            if (user == null)
            {
                return BadRequest(new {message = "Error while registering"});
            }

            return Ok();

        }

    }
}
