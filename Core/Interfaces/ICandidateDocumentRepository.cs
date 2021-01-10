using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface ICandidateDocumentRepository
   {
        Task<CandidateDocument> GetDocumentAsync(int id);
        Task<CandidateDocument> GetMainPhotoForUser(string userId);
        Task<IReadOnlyList<CandidateDocument>> GetDocuments(int id);
        Task<CandidateDocument> CreateDocumentAsync(CandidateDocument candidateDocument);
        Task<CandidateDocument> DeleteDocumentAsync(int Id);
        Task<CandidateDocument> UpdateDocumentAsync(CandidateDocument candidateDocument);
        Task<Candidate> GetCandidateByUserIdAsync(string userId);
        Task<Candidate> GetCandidateByIdAsync(int Id);
        Task<CandidateDocument> GetMainDocumentForUserAsync(int Id);
    }
}
