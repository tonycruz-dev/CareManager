using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class JobConfirmed: BaseEntity
    {

        public string GradeDetails { get; set; }
        public string PhotoUrl { get; set; }
        public string Message { get; set; }

        public DateTime JobDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string JobAddress { get; set; }
        public string PaymentDescription { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public bool? FinishShift { get; set; }
        public bool? LostShift { get; set; }

        public TimeDetail TimeDetail { get; set; }
        public int TimeDetailId { get; set; }

        public PaymentType PaymentType { get; set; }
        public int PaymentTypeId { get; set; }

        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public Agency Agency { get; set; }
        public int AgencyId { get; set; }

        public JobToRequest JobToRequest { get; set; }
        public int JobToRequestId { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public ClientLocation ClientLocation { get; set; }
        public int ClientLocationId { get; set; }

        public ShiftState ShiftState { get; set; }
        public int ShiftStateId { get; set; }

        public AppUser AppUserPosted { get; set; }
        public string AppUserPostedId { get; set; }

        public AppUser AppUserCandidate { get; set; }
        public string AppUserCandidateId { get; set; }
        
        public int AriaId { get; set; }
        public Aria Aria { get; set; }
        
        public AttributeDetail AttributeDetail { get; set; }
        public int AttributeDetailId { get; set; }

        public JobType JobType { get; set; }
        public int JobTypeId { get; set; }

    }
}
