using System;

namespace API.Dtos
{
    public class JobToRequestToReturnDto
    {
        public int Id { get; set; }
        public int NumberCandidate { get; set; }
        public DateTime JobDateStart { get; set; }
        public DateTime JobDateEnd { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int NumberApplied { get; set; }

        public string TimeDetail { get; set; }
        public int TimeDetailId { get; set; }

        public String JobType { get; set; }
        public int JobTypeId { get; set; }
        public string PaymentType { get; set; }
        public int PaymentTypeId { get; set; }
        public string Agency { get; set; }
        public int AgencyId { get; set; }

        public int GradeId { get; set; }
        public string Grade { get; set; }

        public string ClientLocation { get; set; }
        public int ClientLocationId { get; set; }

        public string AttributeDetail { get; set; }
        public int AttributeDetailId { get; set; }

        public string AppUser { get; set; }
        public string AppUserId { get; set; }

        public string ShiftState { get; set; }
        public int ShiftStateId { get; set; }
        public int AriaId { get; set; }
        public string Aria { get; set; }
    }
}
