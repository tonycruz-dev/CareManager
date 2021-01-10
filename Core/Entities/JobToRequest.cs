using System;

namespace Core.Entities
{
    public class JobToRequest : BaseEntity
    {
        public int NumberCandidate { get; set; }
        public DateTime JobDateStart { get; set; }
        public DateTime JobDateEnd { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int NumberApplied { get; set; }

        public TimeDetail TimeDetail { get; set; }
        public int TimeDetailId { get; set; }

        public JobType JobType { get; set; }
        public int JobTypeId { get; set; }
        
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public ClientLocation ClientLocation { get; set; }
        public int ClientLocationId { get; set; }

        public AttributeDetail AttributeDetail { get; set; }
        public int AttributeDetailId { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public ShiftState ShiftState { get; set; }
        public int ShiftStateId { get; set; }

        public PaymentType PaymentType { get; set; }
        public int PaymentTypeId { get; set; }

        public int AriaId { get; set; }
        public Aria Aria { get; set; }

    }
}
