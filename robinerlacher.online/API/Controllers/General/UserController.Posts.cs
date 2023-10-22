using API.Data.General;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(
            [FromHeader] string firstName,
            [FromHeader] string lastName, 
            [FromHeader] string userName, 
            [FromHeader] string eMail, 
            [FromHeader] string password, 
            [FromHeader] string question, 
            [FromHeader] string answer)
        {
            if (_userService.EmailAlreadyUsed(eMail))
            {
                return BadRequest("Email already used");
            }
            if (_userService.UsernameAlreadyUsed(userName))
            {
                return BadRequest("username already used");
            }
            User user = new();
            UserSecurityQuestion userSecQuestion = new();
            userSecQuestion.Question = question;
            userSecQuestion.Answer = answer;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = userName;
            user.Email = eMail;
            user.Password = password;
            user.RegisterDate = DateTime.Now;
            user.SecurityQuestion = userSecQuestion;
            UserDTO result = await _userService.AddAsync(user, userSecQuestion);
            return Ok(result);
        }

        [EnableCors]
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
    }
}
