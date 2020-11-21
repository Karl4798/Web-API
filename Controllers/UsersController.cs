using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdMedAPI.Models;
using AdMedAPI.Repository.IRepository;
using AdMedAPI.Dtos.Models;
using Microsoft.AspNetCore.Http;
using AdMedAPI.Models.Dtos;
using AutoMapper;
using System.Collections.Generic;

namespace AdMedAPI.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/Users")]
    //[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticates a user account.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthenticationDto model)
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
        public IActionResult Register([FromBody] UserRegisterDto model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new {message = "Username already exists"});
            }
            bool passwordsMatch = _userRepo.DoPasswordsMatch(model.Password, model.ConfirmPassword);
            if (!passwordsMatch)
            {
                return Unauthorized(new { message = "Passwords do not match" });
            }
            var user = _userRepo.Register(model.Username, model.Password, model.FirstName, model.LastName);
            if (user == null)
            {
                return BadRequest(new {message = "Error while registering"});
            }
            return Ok();
        }

        /// <summary>
        /// Resets a user password.
        /// </summary>
        [HttpPost("resetpassword")]
        [Authorize(Roles = "Admin,Resident")]
        public IActionResult ResetPassword([FromBody] UserResetPasswordDto model)
        {
            bool passwordsMatch = _userRepo.DoPasswordsMatch(model.Password, model.ConfirmPassword);
            if (!passwordsMatch)
            {
                return BadRequest(new { message = "Passwords do not match" });
            }
            var reset = _userRepo.ResetPassword(model.Username, model.Password, model.ConfirmPassword, model.ExistingPassword);
            if (reset == false)
            {
                return BadRequest(new { message = "Error while resetting password" });
            }
            return Ok();
        }

        /// <summary>
        /// Get an individual user by username.
        /// </summary>
        /// <param name="username">The email of the user</param>
        /// <returns></returns>
        [HttpGet("{username}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin,Resident")]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(string username)
        {
            var obj = _userRepo.GetUser(username);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Get an individual user by id.
        /// </summary>
        /// <param name="username">The id of the user</param>
        /// <returns></returns>
        [HttpGet("{username}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin,Resident")]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int id)
        {
            var obj = _userRepo.GetUser(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Fetches a list of all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(List<UserUpdateDto>))]
        public IActionResult GetUsers()
        {
            var objList = _userRepo.GetUsers();
            var objDto = new List<UserUpdateDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<UserUpdateDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Updates a specific user account.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{UserId:int}", Name = "UpdateUserAccount")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Resident")]
        public IActionResult UpdateUser(int UserId, [FromBody] UserUpdateDto UserUpdateDto)
        {
            if (UserUpdateDto == null || UserId != UserUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }
            var UserObj = _mapper.Map<User>(UserUpdateDto);
            if (!_userRepo.UpdateUser(UserObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {UserUpdateDto.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}