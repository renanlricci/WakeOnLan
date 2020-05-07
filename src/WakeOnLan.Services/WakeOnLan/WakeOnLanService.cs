using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using WakeOnLan.Domain.Interfaces.Services;

namespace WakeOnLan.Services.WakeOnLan
{
    public sealed class WakeOnLanService : IWakeOnLanService
    {
        public async Task WakeUp(string macAddress, string ipAddress, string subnetMask, ushort port)
        {
            var client = new UdpClient {EnableBroadcast = true};

            var dataGram = new byte[102];

            for (var i = 0; i <= 5; i++)
                dataGram[i] = 0xff;

            string[] macDigits = null;
            macDigits = macAddress.Split(macAddress.Contains("-") ? '-' : ':');

            if (macDigits.Length != 6)
                throw new ArgumentException("Incorrect MAC address supplied!");

            const int start = 6;
            for (var i = 0; i < 16; i++)
            for (var x = 0; x < 6; x++)
                dataGram[start + i * 6 + x] = (byte)Convert.ToInt32(macDigits[x], 16);

            var address = IPAddress.Parse(ipAddress);
            var mask = IPAddress.Parse(subnetMask);
            var broadcastAddress = address.GetBroadcastAddress(mask);

            for (var i = 0; i < 10; i++)
                client.Send(dataGram, dataGram.Length, broadcastAddress.ToString(), port);
        }
    }


    
}
