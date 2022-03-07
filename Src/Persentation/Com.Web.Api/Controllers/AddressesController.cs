using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Com.Application.Addresses.Commands;
using Com.Application.Addresses.Queries;
using Com.Application.Common.Enums;

namespace Com.Web.Api.Controllers;

public class AddressesController : BaseController
{
    /// <summary>
    /// Handles the addresses for any entity
    /// </summary>
    /// <param name="belongsTo">The entity which has the address</param>
    /// <param name="type">1- User \ 2- Shop</param>
    /// <returns></returns>

    [HttpGet("{belongsTo}/{type}")]
    [AllowAnonymous]
    public async Task<ActionResult> Get(Guid belongsTo, AddressTypes type) => Ok(await Mediator.Send(new AddressDetailsQuery { BelongTo = belongsTo, Type = type }));

    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert(UpsertAddressCommand command) => Ok(await Mediator.Send(command));
}
