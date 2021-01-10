using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IJobRequestRepository
    {
        Task<IReadOnlyList<JobToRequest>> GetJobToRequestByAgency(int agencyId);
        Task<IReadOnlyList<JobToRequest>> GetJobToRequestByUser(string userId);
        Task<JobToRequest> CreateJobToRequestAsync(JobToRequest jobToRequest);
        Task<JobToRequest> DeleteJobToRequestAsync(int Id);
        Task<JobToRequest> GetJobToRequestByIdAsync(int Id);
        Task<JobToRequest> UpdateJobToRequestAsync(JobToRequest jobToRequest);
    }
}
