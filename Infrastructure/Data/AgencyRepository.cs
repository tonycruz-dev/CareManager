using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly CareManagerContext _context;
        public AgencyRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<Agency> AddAgencyAsync(Agency agency)
        {
            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();
            return agency;
        }

        public async Task<Agency> DeleteAgencyAsync(Agency agency)
        {
            var cpd = await _context.Agencies.FirstOrDefaultAsync(cp => cp.Id == agency.Id);
            _context.Agencies.Remove(cpd);
            await _context.SaveChangesAsync();
            return cpd;
        }

        public async Task<IReadOnlyList<Agency>> GetAgencies()
        {
            return await _context.Agencies.ToListAsync();
        }

        public async Task<Agency> GetAgencyAsync(int Id)
        {
            return await _context.Agencies.FirstOrDefaultAsync(ag => ag.Id == Id);
        }

        public async Task<IReadOnlyList<Agency>> GetTopAgencies(int records)
        {
            return await _context.Agencies.Take(records).ToListAsync();
        }

        public async Task<Agency> UpdateAgencyAsync(Agency agency)
        {
            _context.Entry(agency).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return agency;
        }
    }
}
