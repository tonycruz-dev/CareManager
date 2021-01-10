using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAgencyDocumentRepository
    {
        Task<AgencyDocument> GetDocumentAsync(int id);
        Task<IReadOnlyList<AgencyDocument>> GetDocuments(int id);
        Task<AgencyDocument> CreateDocumentAsync(AgencyDocument candidateDocument);
        Task<AgencyDocument> DeleteDocumentAsync(int Id);
        Task<AgencyDocument> UpdateDocumentAsync(AgencyDocument candidateDocument);
        Task<Agency> GetAgentByEmailAsync(string email);
        Task<AgencyDocument> GetMainDocumentForUserAsync(int Id);
    }
}
