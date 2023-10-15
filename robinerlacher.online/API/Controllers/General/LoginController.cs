using API.Services.GeneralService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    [ApiController]
    [Route("general/[controller]/[action]")]
    public partial class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public LoginController(ILoginService applicationService, IMapper mapper)
        {
            _loginService = applicationService;
            _mapper = mapper;
        }
    }
}
