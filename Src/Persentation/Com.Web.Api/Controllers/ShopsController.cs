using System;
using System.Threading.Tasks;
using Com.Application.Faculties.Commands;
using Com.Application.Shops.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Web.Api.Controllers
{
    public class ShopsController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] ShopsListQuery request = null)
        {
            return Ok(await Mediator.Send(request));
        }

        [AllowAnonymous]
        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id) => Ok(await Mediator.Send(new ShopDetailsQuery { Id = id }));

        // POST: api/Faculties
        [HttpPost]
        public async Task<ActionResult> Post(UpsertShopCommand request) => Ok(await Mediator.Send(request));

        // DELETE: api/Faculties/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await Mediator.Send(new DeleteShopCommand { Id = id });
    }
}