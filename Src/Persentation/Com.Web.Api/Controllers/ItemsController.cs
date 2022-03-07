using System;
using System.Threading.Tasks;
using Com.Application.Items.Commands;
using Com.Application.Items.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Web.Api.Controllers
{
    public class ItemsController : BaseController
    {
        // GET: api/items
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ItemsListQuery request) => Ok(await Mediator.Send(request));

        // Get: api/items/entryId
        [AllowAnonymous]
        [HttpGet("{entryId?}/{id?}")]
        public async Task<IActionResult> GetByEntryId(Guid entryId, Guid id) 
            => Ok(await Mediator.Send(new ItemDetailsQuery { EntryId = entryId, Id = id }));

        // Post: api/items
        [HttpPost]
        public async Task<ActionResult> Post(UpsertItemCommand request) => Ok(await Mediator.Send(request));

        // Get: api/items/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) => Ok(await Mediator.Send(new DeleteItemCommand { Id = id }));
    }
}