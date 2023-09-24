using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            List<User> result = await _userService.DeleteUser(id);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }
    }
}
