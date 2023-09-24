using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(string firstName, string lastName, string userName, string eMail, string password, string question, string answer)
        {
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
            User result = await _userService.AddUser(user, userSecQuestion);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            List<User> result = await _userService.UpdateUser(id, user);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }
    }
}
