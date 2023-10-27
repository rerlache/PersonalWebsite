using API.Data.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class UserController
    {
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> DeleteUser(int id)
        {
            List<UserDTO> result = await _userService.DeleteAsync(id);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }
    }
}
