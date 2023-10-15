using API.Data.General;
using API.Services.GeneralService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.General
{
    public partial class LoginController
    {
        private string GetIp()
        {
            string ip = Request.HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
            if (ip == null)
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            return ip;
        }

        [HttpGet]
        public async Task<ActionResult<Tuple<string, UserDTO>>> WithData([FromHeader] string userName, [FromHeader] string password)
        {
            string ip = Request.HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
            if (ip == null)
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            Tuple<string, UserDTO> result = await _loginService.WithDataAsync(userName, password, ip);
            if (result.Item2 == null)
            {
                return BadRequest(result.Item1);
            }
            return Ok(new Tuple<string, UserDTO>(result.Item1, result.Item2));
        }
        [HttpGet]
        public async Task<ActionResult<UserDTO>> WithToken([FromHeader] string token)
        {
            string ip = Request.HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
            if (ip == null)
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            UserDTO result = await _loginService.WithTokenAsync(token, ip);
            if(result ==  null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<UserLoginHistoryDTO>>> GetHistory([FromHeader] int userId)
        {
            return Ok(await _loginService.GetLoginHistoryAsync(userId));
        }
    }
}
