using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClientLocation
    {
        Task<IReadOnlyList<ClientLocation>> GetClientLocationByAgency(int agencyId);
        Task<IReadOnlyList<ClientLocation>> GetClientLocationByUser(string userId);
        Task<ClientLocation> CreateClientLocationAsync(ClientLocation clientLocation);
        Task<ClientLocation> DeleteClientLocationAsync(int Id);
        Task<ClientLocation> UpdateClientLocationAsync(ClientLocation clientLocation);
    }
}
