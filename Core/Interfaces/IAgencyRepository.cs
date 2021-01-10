using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAgencyRepository
    {
        Task<Agency> AddAgencyAsync(Agency agency);
        Task<Agency> GetAgencyAsync(int Id);
        Task<Agency> UpdateAgencyAsync(Agency agency);
        Task<Agency> DeleteAgencyAsync(Agency agency);
        Task<IReadOnlyList<Agency>> GetAgencies();
        Task<IReadOnlyList<Agency>> GetTopAgencies(int records);
    }
}
