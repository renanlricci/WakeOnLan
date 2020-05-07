namespace WakeOnLan.Domain.Commands.WakeUp.MainDevice
{
    public sealed class WakeUpMainCommandResponse
    {
        public string IpAddress { get; set; }

        public WakeUpMainCommandResponse(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        public static implicit operator WakeUpMainCommandResponse(string ipAddress) =>
            new WakeUpMainCommandResponse(ipAddress);
    }
}
