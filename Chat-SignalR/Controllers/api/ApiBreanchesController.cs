using Chat_SignalR.Models;
using Chat_SignalR.Models.ApiModels;
using Chat_SignalR.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_SignalR.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ApiBreanchesController : Controller
    {
        private readonly BreanchRepository repository;

        public ApiBreanchesController(BreanchRepository _rep)
        {
            repository = _rep;
        }

        [HttpPost("createBreanch")]
        public async Task<IActionResult> RegisterNewBreanch([FromBody] BreanchModel model)
        {
            if (ModelState.IsValid)
            {
                var b = new Breanch() { Name = model.Name, Description = model.Description, PublicId = Guid.NewGuid() };
                await repository.Add(b);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        public async Task<IActionResult> DeleteBreanch(int id)
        {
            await repository.Remove(id);
            return Ok();
        }
    }
}
