using Core.Entities;
using Core.EntityHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IHRManagerRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        // job request stat here
        Task<PagedList<JobToRequest>> GetJobToRequestByAgency(int agencyId, JobRequestParams jobRequestParams);
        Task<PagedList<JobToRequest>> GetJobToRequestByUser(string userId, JobRequestParams jobRequestParams);
        Task<JobToRequest> CreateJobToRequestAsync(JobToRequest jobToRequest);
        Task<JobToRequest> DeleteJobToRequestAsync(int Id);
        Task<JobToRequest> GetJobToRequestByIdAsync(int Id);
        Task<JobToRequest> UpdateJobToRequestAsync(JobToRequest jobToRequest);
    }
}
