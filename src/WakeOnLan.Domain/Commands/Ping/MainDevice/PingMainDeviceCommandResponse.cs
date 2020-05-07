using System.Collections.Generic;

namespace WakeOnLan.Domain.Commands.Ping.MainDevice
{
    public sealed class PingMainDeviceCommandResponse
    {
        public PingMainDeviceCommandResponse(string ipAddress, IEnumerable<string> pingStatus)
        {
            IpAddress = ipAddress;
            PingStatus = pingStatus;
        }
        public string IpAddress { get; set; }
        public IEnumerable<string> PingStatus { get; set; }

    }
}
