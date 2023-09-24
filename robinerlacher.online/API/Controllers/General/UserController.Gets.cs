using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            User result = await _userService.GetUserById(id);
            if (result is null)
            {
                return NotFound($"No user with ID {id} found.");
            }
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserSecurityQuestion>> GetQuestionForUser(int userId)
        {
            return Ok(await _userService.GetSecurityQuestionForUser(userId));
        }
    }
}
