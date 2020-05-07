using MediatR;

namespace WakeOnLan.Domain.Commands.Ping.MainDevice
{
    public sealed class PingMainDeviceCommand : IRequest<PingMainDeviceCommandResponse>
    {
    }
}
