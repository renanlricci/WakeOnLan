using MediatR;
using Microsoft.Extensions.Options;
using WakeOnLan.CrossCutting.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace WakeOnLan.Domain.Commands.Ping.MainDevice
{
    public sealed class PingMainDeviceCommandHandler : IRequestHandler<PingMainDeviceCommand, PingMainDeviceCommandResponse>
    {
        private readonly AppSettings _appSettings;

        public PingMainDeviceCommandHandler(IOptionsMonitor<AppSettings> appSettings) => _appSettings = appSettings.CurrentValue;

        public async Task<PingMainDeviceCommandResponse> Handle(PingMainDeviceCommand request, CancellationToken cancellationToken)
        {
            var pingCount = _appSettings.Ping.Count;
            var pingClient = new System.Net.NetworkInformation.Ping();

            var pings = new string[pingCount];
            for (var i = 0; i < pingCount; i++)
            {
                var ping = await pingClient.SendPingAsync(_appSettings.Ping.MainIp);
                pings[i] = ping.Status.ToString();
            }

            return new PingMainDeviceCommandResponse(_appSettings.Ping.MainIp, pings);
        }
    }
}
