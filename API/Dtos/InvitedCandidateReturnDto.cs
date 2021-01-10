using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InvitedCandidateReturnDto
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

        public string TimeDetail { get; set; }
        public int TimeDetailId { get; set; }

        public string PaymentType { get; set; }
        public int PaymentTypeId { get; set; }

        public string Candidate { get; set; }
        public int CandidateId { get; set; }

        public string Agency { get; set; }
        public int AgencyId { get; set; }

        public int JobToRequestId { get; set; }

        public int GradeId { get; set; }
        public string Grade { get; set; }

        public string ClientLocation { get; set; }
        public int ClientLocationId { get; set; }

        public string ShiftState { get; set; }
        public int ShiftStateId { get; set; }

        public string AppUserPosted { get; set; }
        public string AppUserPostedId { get; set; }

        public string AppUserCandidate { get; set; }
        public string AppUserCandidateId { get; set; }

        public int AriaId { get; set; }
        public string Aria { get; set; }

        public string AttributeDetail { get; set; }
        public int AttributeDetailId { get; set; }

        public string JobType { get; set; }
        public int JobTypeId { get; set; }
    }

    public class InvitedCandidateFromCandidateDto
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

        public string PaymentType { get; set; }
        public int PaymentTypeId { get; set; }

        public string Candidate { get; set; }
        public int CandidateId { get; set; }

        public string Agency { get; set; }
        public int AgencyId { get; set; }
        public string AvatarAgencyUrl { get; set; }

        public int JobToRequestId { get; set; }

        public int GradeId { get; set; }
        public string Grade { get; set; }


        public string ShiftState { get; set; }
        public int ShiftStateId { get; set; }


        public int AriaId { get; set; }
        public string Aria { get; set; }

        public string AttributeDetail { get; set; }
        public int AttributeDetailId { get; set; }

        public string JobType { get; set; }
        public int JobTypeId { get; set; }
    }
}
