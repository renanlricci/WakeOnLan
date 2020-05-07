using MediatR;

namespace WakeOnLan.Domain.Commands.WakeUp.MainDevice
{
    public sealed class WakeUpMainCommand : IRequest<WakeUpMainCommandResponse>
    {
    }
}
