using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        Task<string> CreateTokenAsync(AppUser user);
    }
}
