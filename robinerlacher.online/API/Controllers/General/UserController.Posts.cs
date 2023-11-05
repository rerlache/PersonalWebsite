using API.Data.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO register)
        {
            if (_userService.EmailAlreadyUsed(register.eMail))
            {
                return BadRequest("Email already used");
            }
            if (_userService.UsernameAlreadyUsed(register.userName))
            {
                return BadRequest("username already used");
            }
            User user = new();
            UserSecurityQuestion userSecQuestion = new();
            userSecQuestion.Question = register.question;
            userSecQuestion.Answer = register.answer;
            user.FirstName = register.firstName;
            user.LastName = register.lastName;
            user.UserName = register.userName;
            user.Email = register.eMail;
            user.Password = register.password;
            user.RegisterDate = DateTime.Now;
            user.SecurityQuestion = userSecQuestion;
            UserDTO result = await _userService.AddAsync(user, userSecQuestion);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<UserDTO>> Update(int id, User user)
        {
            UserDTO result = await _userService.UpdateAsync(id, user);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignUpApp([FromBody] AppUserDTO ids)
        {
            UserDTO? user = await _userService.AssignAppToUser(ids.AppId, ids.UserId);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> ResetPassword([FromBody] PasswordResetDTO passwordReset)
        {
            UserDTO result = await _userService.ResetPassword(passwordReset);
            if (result is null)
            {
                return BadRequest("couldn't reset password");
            }
            return Ok(result);
        }
    }
}
