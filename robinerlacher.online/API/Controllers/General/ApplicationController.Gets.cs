using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class ApplicationController
    {
        [HttpGet]
        public async Task<ActionResult<List<Application>>> GetAllApplications()
        {
            return Ok(await _applicationService.GetAllApplicationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetAppById(int id)
        {
            Application result = await _applicationService.GetApplicationByIdAsync(id);
            return Ok(result);
        }
        
        [HttpGet("{name}")]
        public async Task<ActionResult<Application>> GetAppByName(string name)
        {
            Application result = await _applicationService.GetApplicationByNameAsync(name);
            return Ok(result);
        }
    }
}
