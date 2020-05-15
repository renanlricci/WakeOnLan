using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WakeOnLan.Domain.Commands.Ping.MainDevice;
using System.Threading.Tasks;

namespace WakeOnLan.Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/Ping")]
    public class PingController : BaseController<PingController>
    {
        public PingController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("Main")]
        public async Task<IActionResult> PingMainAsync() => await CreateResponse(async () => await _mediator.Send(new PingMainDeviceCommand()));
    }
}