using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class ApplicationController
    {
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Application>> AddApplication([FromBody] Application app)
        {
            Application result = await _applicationService.AddApplicationAsync(app);
            return Ok(result);
        }
        [EnableCors]
        [HttpPost("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(int id, string name = "", string description = "", string url = "")
        {
            Application application = await _applicationService.UpdateApplicationAsync(id, new Application { Name = name, Description = description, Url = url});
            return Ok(application);
        }
    }
}
