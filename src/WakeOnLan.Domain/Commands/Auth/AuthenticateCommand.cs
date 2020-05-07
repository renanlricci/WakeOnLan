using MediatR;

namespace WakeOnLan.Domain.Commands.Auth
{
    public sealed class AuthenticateCommand : IRequest<AuthenticateCommandResponse>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
