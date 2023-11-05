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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetQuestionForUser([FromHeader] string username, [FromHeader] string email)
        {
            PasswordResetDTO passwordResetDTO = new PasswordResetDTO();
            passwordResetDTO.Username = username;
            passwordResetDTO.Email = email;
            string? question = await _userService.GetSecurityQuestionForUserAsync(passwordResetDTO);
            if (String.IsNullOrEmpty(question))
            {
                return NotFound("username/email combination not found");
            }
            return Ok(question);
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckAnswer([FromHeader] string username, [FromHeader] string answer)
        {
            PasswordResetDTO passwordResetDTO = new();
            passwordResetDTO.Username = username;
            passwordResetDTO.Answer = answer;
            int userId = await _userService.GetUserIdByUsername(passwordResetDTO.Username);
            if(userId == -1)
            {
                return BadRequest("user not found");
            }
            bool answeredCorrect = await _userService.CheckAnswerToQuestion(userId, passwordResetDTO);
            if(!answeredCorrect)
            {
                return BadRequest("wrong answer");
            }
            return Ok("everything is fine, proceed with pw reset");
        }
    }
}
