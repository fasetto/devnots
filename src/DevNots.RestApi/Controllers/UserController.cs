using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto user)
        {
            var response = await userService.RegisterAsync(user);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto request)
        {
            var response = await userService.DeleteUserAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "User deleted."});
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int limit = 20)
        {
            var response = await userService.GetUsersAsync(new UserListDto()
            {
                Limit = limit,
            });

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }

    }
}
