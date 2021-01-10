using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
   public class JobToRequestPaginationSpec :BaseSpecification<JobToRequest>
    {
        public JobToRequestPaginationSpec(JobToRequesSpecParams jobToRequesParams)
             : base(x =>
                 (string.IsNullOrEmpty(jobToRequesParams.Search) || 
             x.ShiftState.ShiftDetails.ToLower().Contains(jobToRequesParams.Search) ||
             x.ClientLocation.Address1.ToLower().Contains(jobToRequesParams.Search)) && 
             x.AppUserId == jobToRequesParams.UserId)
                
        {

            AddInclude(x => x.TimeDetail);
            AddInclude(x => x.ClientLocation);
            AddInclude(x => x.AttributeDetail);
            AddInclude(x => x.ShiftState);
            AddInclude(x => x.JobType);
            AddInclude(x => x.PaymentType);
            AddInclude(x => x.Agency);
            AddInclude(x => x.Grade);
            AddInclude(x => x.Aria);
            AddInclude(x => x.AppUser);

            ApplyPaging(jobToRequesParams.PageSize * (jobToRequesParams.PageIndex - 1), jobToRequesParams.PageSize);

            if (!string.IsNullOrEmpty(jobToRequesParams.Sort))
            {
                switch (jobToRequesParams.Sort)
                {
                    case "dateAsc":
                        AddOrderBy(jr => jr.JobDateStart);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(jr => jr.JobDateStart);
                        break;
                    default:
                        AddOrderBy(n => n.Grade.GradeName);
                        break;
                }
            }

        }
        public JobToRequestPaginationSpec(string userId) : base(o => o.AppUserId == userId)
        {
            AddInclude(x => x.TimeDetail);
            AddInclude(x => x.ClientLocation);
            AddInclude(x => x.AttributeDetail);
            AddInclude(x => x.ShiftState);
            AddInclude(x => x.JobType);
            AddInclude(x => x.PaymentType);
            AddInclude(x => x.Agency);
            AddInclude(x => x.Grade);
            AddInclude(x => x.Aria);
            AddInclude(x => x.AppUser);
        }
    }
}
