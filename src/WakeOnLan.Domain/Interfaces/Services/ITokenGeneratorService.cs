using System.Threading.Tasks;

namespace WakeOnLan.Domain.Interfaces.Services
{
    public interface ITokenGeneratorService
    {
        Task<string> GenerateToken(string username, params string[] userClaims);
    }
}
