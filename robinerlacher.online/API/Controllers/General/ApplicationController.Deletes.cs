using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.General
{
    public partial class ApplicationController
    {
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Application>>> DeleteApplication(int id)
        {
            return Ok(await _applicationService.DeleteApplicationAsync(id));
        }
    }
}
