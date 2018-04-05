using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request;
using PosRi.Models.Request.User;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private const string Route = "api/user";
     
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                var results = Mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userRepository.GetUserAsync(id);
                if (user == null)
                    return NotFound();

                var result = Mapper.Map<UserDto>(user);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            try
            {
                if (!User.HasClaim(c => c.Type == "UserId")) return Unauthorized();
                int.TryParse(User.Claims.First(claim => claim.Type == "UserId").Value, out int userId);
                var user = await _userRepository.GetUserAsync(userId);
                var userDto = Mapper.Map<UserDto>(user);
                return Ok(userDto);

            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/info - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] NewUserDto newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _userRepository.IsDuplicateUserAsync(newUser))
                {
                    ModelState.AddModelError("username", "Username already exists");
                    return BadRequest(ModelState);
                }

                var userId = await _userRepository.AddUserAsync(newUser);

                if (userId > 0)
                {
                    return Ok(userId);
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"POST {Route}/ - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] EditUserDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _userRepository.UserExistsAsync(user.Id))
                {
                    ModelState.AddModelError("user", "User not found");
                    return BadRequest(ModelState);
                }

                if (await _userRepository.IsDuplicateUserAsync(user))
                {
                    ModelState.AddModelError("username", "Username already exists");
                    return BadRequest(ModelState);
                }

                var wasUserEdited = await _userRepository.EditUserAsync(user);

                if (wasUserEdited)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"PUT {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _userRepository.UserExistsAsync(id))
                {
                    return NotFound();
                }

                var wasUserDeleted = await _userRepository.DeleteUserAsync(id);

                if (wasUserDeleted)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"DELETE {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }
    }
}