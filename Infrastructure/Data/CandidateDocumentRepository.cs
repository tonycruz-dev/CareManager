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
    public class CandidateDocumentRepository : ICandidateDocumentRepository
    {
        private readonly CareManagerContext _context;

        public CandidateDocumentRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<CandidateDocument> CreateDocumentAsync(CandidateDocument candidateDocument)
        {
            _context.CandidateDocuments.Add(candidateDocument);
            await _context.SaveChangesAsync();
            return candidateDocument;
        }

        public async Task<CandidateDocument> DeleteDocumentAsync(int Id)
        {
            var cpd = await _context.CandidateDocuments.FirstOrDefaultAsync(cp => cp.Id == Id);
            _context.CandidateDocuments.Remove(cpd);
            await _context.SaveChangesAsync();
            return cpd;
        }

        public async Task<Candidate> GetCandidateByIdAsync(int Id)
        {
            var candidate = await _context.Candidates.Include(ci => ci.CandidateDocuments).Where(c => c.Id == Id).SingleOrDefaultAsync();
            return candidate;
        }

        public async Task<Candidate> GetCandidateByUserIdAsync(string email)
        {
            var candidate = await _context.Candidates.Include(ci => ci.CandidateDocuments).Where(c => c.Email == email).SingleOrDefaultAsync();
            return candidate;
        }

        public async Task<CandidateDocument> GetDocumentAsync(int id)
        {
            return await _context.CandidateDocuments.FirstOrDefaultAsync(cu => cu.Id == id);
        }

        public async Task<IReadOnlyList<CandidateDocument>> GetDocuments(int id)
        {
            return await _context.CandidateDocuments.Where(cd => cd.CandidateId == id).ToListAsync();
        }

        public async Task<CandidateDocument> GetMainDocumentForUserAsync(int Id)
        {
            return await _context.CandidateDocuments.FirstOrDefaultAsync(cp => cp.Id == Id);
        }

        public Task<CandidateDocument> GetMainPhotoForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CandidateDocument> UpdateDocumentAsync(CandidateDocument candidateDocument)
        {
            _context.Entry(candidateDocument).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return candidateDocument;
        }
    }
}
