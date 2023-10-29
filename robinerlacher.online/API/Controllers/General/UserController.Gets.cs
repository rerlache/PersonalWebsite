using API.Data.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {

            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            UserDTO result = await _userService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound($"No user with ID {id} found.");
            }
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserSecurityQuestion>> GetQuestionForUser(int userId)
        {
            return Ok(await _userService.GetSecurityQuestionForUserAsync(userId));
        }
    }
}
