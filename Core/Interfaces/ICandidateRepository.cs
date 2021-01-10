using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidateAsync(int id);
        Task<Candidate> CreateCandidateAsync(Candidate candidate);
        Task<Candidate> DeleteCandidateAsync(int Id);
        Task<Candidate> UpdateCandidateAsync(Candidate candidate);
        Task<Candidate> GetCandidateByUserIdAsync(string userId);
    }
}
