using API.Data.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class LoginController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Tuple<string, UserDTO>>> WithData([FromBody] LoginDTO login)
        {
            if (login.userName == null || login.password == null)
            {
                return NoContent();
            }
            string? ip = HttpContext.Connection.RemoteIpAddress.ToString();
            if (ip == "127.0.0.1")
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            Tuple<string, UserDTO> result = await _loginService.WithDataAsync(login, String.IsNullOrEmpty(ip) ? "0.0.0.0" : ip);
            if (result.Item2 == null)
            {
                return BadRequest(result.Item1);
            }
            return Ok(new Tuple<string, UserDTO>(result.Item1, result.Item2));
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> WithToken([FromBody] string token)
        {
            string? ip = HttpContext.Connection.RemoteIpAddress.ToString();
            if (ip == "127.0.0.1")
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            UserDTO result = await _loginService.WithTokenAsync(token, ip);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
