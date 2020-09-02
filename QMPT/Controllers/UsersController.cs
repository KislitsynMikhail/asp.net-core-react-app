using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Models.Requests.Users;
using QMPT.Data.Services;
using QMPT.Data.Models;
using QMPT.Models.Responses;
using QMPT.Helpers;

namespace QMPT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult PostNewUser([FromBody] UserRequest userRequest)
        {
            var newUser = new User(userRequest);

            usersService.Insert(newUser);

            return Created($"api/Users/{newUser.Id}", new UserResponse(newUser));
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserDataById(
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.invalidParameters)]
            int userId)
        {
            return GetUserResponseById(userId);
        }

        [HttpGet]
        public IActionResult GetUserData()
        {
            return GetUserResponseById(User.GetId());
        }

        private IActionResult GetUserResponseById(int userId)
        {
            var user = usersService.Get(userId);

            return Ok(new UserResponse(user));
        }
    }
}
