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
    public class ClientLocationRepository : IClientLocation
    {
        private readonly CareManagerContext _context;

        public ClientLocationRepository(CareManagerContext context)
        {
            _context = context;
        }

        public async Task<ClientLocation> CreateClientLocationAsync(ClientLocation clientLocation)
        {
            _context.ClientLocations.Add(clientLocation);
            await _context.SaveChangesAsync();
            return clientLocation;
        }

        public async Task<ClientLocation> DeleteClientLocationAsync(int Id)
        {
            var removeCL = _context.ClientLocations.SingleOrDefault(c => c.Id == Id);
            _context.ClientLocations.Remove(removeCL);
            await _context.SaveChangesAsync();
            return removeCL;
        }

        public async Task<IReadOnlyList<ClientLocation>> GetClientLocationByAgency(int agencyId)
        {
            return await _context.ClientLocations.Where(cl => cl.AgencyId == agencyId).ToListAsync();
        }

        public async Task<IReadOnlyList<ClientLocation>> GetClientLocationByUser(string userId)
        {
            var agency = await _context.Agencies.SingleOrDefaultAsync(ag => ag.AppUserId == userId);

            return await _context.ClientLocations.Where(cl => cl.AgencyId == agency.Id).ToListAsync();
        }

        public async Task<ClientLocation> UpdateClientLocationAsync(ClientLocation clientLocation)
        {
            var updateCL = await _context.ClientLocations.FirstOrDefaultAsync(cl => cl.Id == clientLocation.Id);
            updateCL = clientLocation;
            await _context.SaveChangesAsync();
            return clientLocation;
        }
    }
}
