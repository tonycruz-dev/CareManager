using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InvitedCandidateInserOrUpdateDto
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string Message { get; set; }
        public bool? Accept { get; set; }
        public bool? Reject { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishTime { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string RespondTime { get; set; }
        public DateTime JobDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string JobAddress { get; set; }
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
