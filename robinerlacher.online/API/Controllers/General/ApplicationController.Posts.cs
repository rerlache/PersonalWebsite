using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Application>> AddApplication(string name, string description, string url)
        {
            Application application = new Application(){ Name = name, Description = description, Url = url};
            Application result = await _applicationService.AddApplicationAsync(application);
            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(int id, string name = "", string description = "", string url = "")
        {
            Application application = await _applicationService.UpdateApplicationAsync(id, new Application { Name = name, Description = description, Url = url});
            return Ok(application);
        }
    }
}
