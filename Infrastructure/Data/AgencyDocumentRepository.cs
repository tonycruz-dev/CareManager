using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AgencyDocumentRepository : IAgencyDocumentRepository
    {
        private readonly CareManagerContext _context;

        public AgencyDocumentRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<AgencyDocument> CreateDocumentAsync(AgencyDocument agencyDocument)
        {
            _context.AgencyDocuments.Add(agencyDocument);
            await _context.SaveChangesAsync();
            return agencyDocument;
        }

        public async Task<AgencyDocument> DeleteDocumentAsync(int Id)
        {
            var cpd = await _context.AgencyDocuments.FirstOrDefaultAsync(cp => cp.Id == Id);
            _context.AgencyDocuments.Remove(cpd);
            await _context.SaveChangesAsync();
            return cpd;
        }

        public async Task<Agency> GetAgentByEmailAsync(string email)
        {
            var candidate = await _context.Agencies.Include(ci => ci.AgencyDocuments).Where(c => c.Email == email).SingleOrDefaultAsync();
            return candidate;
        }

        public async Task<AgencyDocument> GetDocumentAsync(int id)
        {
            return await _context.AgencyDocuments.FirstOrDefaultAsync(cu => cu.Id == id);
        }

        public async Task<IReadOnlyList<AgencyDocument>> GetDocuments(int id)
        {
            return await _context.AgencyDocuments.Where(ad => ad.AgencyId == id).ToListAsync();
        }

        public async Task<AgencyDocument> GetMainDocumentForUserAsync(int Id)
        {
            return await _context.AgencyDocuments.FirstOrDefaultAsync(cp => cp.Id == Id);
        }

        public async Task<AgencyDocument> UpdateDocumentAsync(AgencyDocument agencyDocument)
        {
            _context.Entry(agencyDocument).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return agencyDocument;
        }
    }
}
