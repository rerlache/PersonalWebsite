using API.Data.General;
using API.Services.GeneralService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace API.Controllers.General
{
    public partial class LoginController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserLoginHistoryDTO>>> GetHistory([FromHeader] string userName)
        {
            return Ok(await _loginService.GetLoginHistoryAsync(userName));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> WithToken([FromHeader] string token)
        {
            string? ip = HttpContext.Connection?.RemoteIpAddress?.ToString();
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
