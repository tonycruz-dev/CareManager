using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
   public class JobToRequestCounterSpec: BaseSpecification<JobToRequest>
    {
        public JobToRequestCounterSpec(JobToRequesSpecParams jobToRequesParams) 
            : base(x =>
                  (string.IsNullOrEmpty(jobToRequesParams.Search) ||
                  x.ShiftState.ShiftDetails.ToLower().Contains(jobToRequesParams.Search) ||
                  x.ClientLocation.Address1.ToLower().Contains(jobToRequesParams.Search)) &&
                  x.AppUserId == jobToRequesParams.UserId)
        {

        }
    }
}
