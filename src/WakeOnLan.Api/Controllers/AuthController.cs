using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WakeOnLan.Domain.Commands.Auth;
using System.Threading.Tasks;

namespace WakeOnLan.Api.Controllers
{

    [Route("api/Auth")]
    public class AuthController : BaseController<AuthController>
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]AuthenticateCommand command) => await CreateResponse(async () => await _mediator.Send(command));
    }
}
