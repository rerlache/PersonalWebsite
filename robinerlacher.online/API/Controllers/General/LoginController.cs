using API.Services.GeneralService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    [ApiController]
    [Route("general/[controller]/[action]")]
    [Authorize]
    public partial class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public LoginController(ILoginService applicationService, IUserService userService)
        {
            _loginService = applicationService;
            _userService = userService;
        }
    }
}
