using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class ApplicationController
    {
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Application>>> DeleteApplication(int id)
        {
            return Ok(await _applicationService.DeleteApplicationAsync(id));
        }
    }
}
