﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WakeOnLan.Domain.Commands.WakeUp.MainDevice;
using System.Threading.Tasks;

namespace WakeOnLan.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/WakeUp")]
    public class WakeUpController : BaseController<WakeUpController>
    {
        public WakeUpController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Main")]
        public async Task<IActionResult> WakeUpMainAsync() => await CreateResponse(async () => await _mediator.Send(new WakeUpMainCommand()));
    }
}
