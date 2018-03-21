using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosRi.Models.Request;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
     
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            var results = Mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return NotFound();

            var result = Mapper.Map<UserDto>(user);

            return Ok(result);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            if (User.HasClaim(c => c.Type == "UserId"))
            {
                int.TryParse(User.Claims.First(claim => claim.Type == "UserId").Value, out int userId);
                var user = await _userService.GetUser(userId);
                var userDto = Mapper.Map<UserDto>(user);
                return Ok(userDto);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(NewUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userService.UserExists(newUser))
            {
                return BadRequest();
            }

            var userId = await _userService.AddUser(newUser);

            if (userId > 0)
            {
                return Ok(userId);
            }

            return StatusCode(500, "An error ocurred in server");
        }

    }
}