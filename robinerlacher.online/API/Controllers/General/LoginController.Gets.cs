using API.Data.General;
using API.Services.GeneralService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class LoginController
    {
        [HttpGet]
        public async Task<ActionResult<Tuple<string, UserDTO>>> WithData([FromHeader] string userName, [FromHeader] string password)
        {
            string ip = Request.HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
            if (ip == null)
            {
                ip = HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            Tuple<string, User> result = await _loginService.WithDataAsync(userName, password, ip);
            if (result.Item2 == null)
            {
                return BadRequest(result.Item1);
            }
            return Ok(new Tuple<string, UserDTO>(result.Item1, _mapper.Map<UserDTO>(result.Item2)));
        }
        [HttpGet]
        public async Task<ActionResult<List<UserLoginHistoryDTO>>> GetHistory([FromHeader] int userId)
        {
            List<UserLoginHistory> history = await _loginService.GetLoginHistoryAsync(userId);
            UserLoginHistoryDTO[] dtoLogins = _mapper.Map <UserLoginHistoryDTO[]>(history).ToArray();
            return Ok(dtoLogins);
        }
    }
}
