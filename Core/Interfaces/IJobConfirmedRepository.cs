using Core.Entities;
using Core.EntityHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJobConfirmedRepository
    {
        Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByAgency(int agencyId);
        Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByUser(string userId);
        Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByJobId(int jobId);
        Task<JobConfirmed> CreateJobConfirmedAsync(JobConfirmed jobConfirmed);
        Task<JobConfirmed> DeleteJobConfirmedAsync(int Id);
        Task<JobConfirmed> UpdateJobConfirmedAsync(JobConfirmed jobConfirmed);
        Task<JobConfirmed> FinalizedJobConfirmedAsync(ConfirmeFinal confirmeFinal);
    }
}
