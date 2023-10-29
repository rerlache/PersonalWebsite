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
    }
}
