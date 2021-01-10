using Core.Entities;

namespace Core.Specifications
{
    public class JobToRequestSpec: BaseSpecification<JobToRequest>
    {
        public JobToRequestSpec()
        {
            AddInclude(x => x.TimeDetail);
            AddInclude(x => x.ClientLocation);
            AddInclude(x => x.AttributeDetail);
            AddInclude(x => x.ShiftState);
            AddInclude(x => x.JobType);
            AddInclude(x => x.PaymentType);
            AddInclude(x => x.Agency);
            AddInclude(x => x.Grade);
            AddInclude(x => x.AppUser);
        }
        public JobToRequestSpec(int id): base(o => o.Id == id)
        {
            AddInclude(x => x.TimeDetail);
            AddInclude(x => x.ClientLocation);
            AddInclude(x => x.AttributeDetail);
            AddInclude(x => x.ShiftState);
            AddInclude(x => x.JobType);
            AddInclude(x => x.PaymentType);
            AddInclude(x => x.Agency);
            AddInclude(x => x.Grade);
            AddInclude(x => x.AppUser);
        }
        public JobToRequestSpec(string userId) : base(o => o.AppUserId == userId)
        {
            AddInclude(x => x.TimeDetail);
            AddInclude(x => x.ClientLocation);
            AddInclude(x => x.AttributeDetail);
            AddInclude(x => x.ShiftState);
            AddInclude(x => x.JobType);
            AddInclude(x => x.PaymentType);
            AddInclude(x => x.Agency);
            AddInclude(x => x.Grade);
            AddInclude(x => x.AppUser);
        }
    }
}
