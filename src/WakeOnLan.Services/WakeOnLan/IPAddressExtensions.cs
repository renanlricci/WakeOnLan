using System;
using System.Net;

namespace WakeOnLan.Services.WakeOnLan
{
    public static class IPAddressExtensions
    {
        public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            var ipAddressBytes = address.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAddressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            var broadcastAddress = new byte[ipAddressBytes.Length];
            for (var i = 0; i < broadcastAddress.Length; i++)
                broadcastAddress[i] = (byte)(ipAddressBytes[i] | (subnetMaskBytes[i] ^ 255));

            return new IPAddress(broadcastAddress);
        }
    }
}
