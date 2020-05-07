using System.Threading.Tasks;

namespace WakeOnLan.Domain.Interfaces.Services
{
    public interface IWakeOnLanService
    {
        Task WakeUp(string macAddress, string ipAddress, string subnetMask, ushort port);
    }
}
