using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class ApplicationController
    {
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Application>> AddApplication([FromBody] Application app)
        {
            Application result = await _applicationService.AddApplicationAsync(app);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(int id, string name = "", string description = "", string url = "")
        {
            Application application = await _applicationService.UpdateApplicationAsync(id, new Application { Name = name, Description = description, Url = url });
            return Ok(application);
        }
    }
}
