using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class JobConfirmedInserOrUpdateDto
    {
        public int Id { get; set; }
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

        public int TimeDetailId { get; set; }
        public int PaymentTypeId { get; set; }
        public int CandidateId { get; set; }
        public int AgencyId { get; set; }

        public int JobToRequestId { get; set; }

        public int GradeId { get; set; }
        public int ClientLocationId { get; set; }
        public int ShiftStateId { get; set; }
        public string AppUserPostedId { get; set; }
        public string AppUserCandidateId { get; set; }

        public int AriaId { get; set; }
        public int AttributeDetailId { get; set; }
        public int JobTypeId { get; set; }
    }
}
