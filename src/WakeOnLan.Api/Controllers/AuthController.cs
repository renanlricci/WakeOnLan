using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WakeOnLan.Domain.Commands.Auth;

namespace WakeOnLan.Api.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : BaseController<AuthController>
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> LoginAsync([FromBody]AuthenticateCommand command) => await CreateResponse(async () => await _mediator.Send(command));
    }
}
