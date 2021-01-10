using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class JobFinishToReturnDto
    {
        public int Id { get; set; }
        public string GradeDetails { get; set; }
        public string PhotoUrl { get; set; }
        public string AgencyPhotoUrl { get; set; }
        public string Message { get; set; }

        public DateTime JobDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string JobAddress { get; set; }
        public decimal HourlyRate { get; set; }
        private decimal _totalAmoun;

        public decimal TotalAmount
        {
            get
            {
                DateTime timeFrom = DateTime.Parse(StartTime);
                DateTime timeTo = DateTime.Parse(EndTime);

                TimeSpan tsFrom = new TimeSpan(timeFrom.Hour, timeFrom.Minute, timeFrom.Second);
                TimeSpan tsTo = new TimeSpan(timeTo.Hour, timeTo.Minute, timeTo.Second);
                var tsCalculateHours = tsTo.Subtract(tsFrom);
                _totalAmoun = tsCalculateHours.Hours * HourlyRate;
                return _totalAmoun; 
            }
            set { _totalAmoun = value; }
        }

        //public string PaymentDescription { get; set; }
        //public string Comment { get; set; }
        //public int Rating { get; set; }
        //public bool? FinishShift { get; set; }
        //public bool? LostShift { get; set; }

        //public string TimeDetail { get; set; }
        // public int TimeDetailId { get; set; }

        //public string PaymentType { get; set; }
        //public int PaymentTypeId { get; set; }

        //public string Candidate { get; set; }
        //public int CandidateId { get; set; }

        //public string Agency { get; set; }
        //public int AgencyId { get; set; }

        //public string JobToRequest { get; set; }
        //public int JobToRequestId { get; set; }

        //public int GradeId { get; set; }
        //public string Grade { get; set; }

        //public string ClientLocation { get; set; }
        //public int ClientLocationId { get; set; }

        //public string ShiftState { get; set; }
        //public int ShiftStateId { get; set; }

        //public string AppUserPosted { get; set; }
        //public string AppUserPostedId { get; set; }

        //public string AppUserCandidate { get; set; }
        //public string AppUserCandidateId { get; set; }

        //public int AriaId { get; set; }
        //public string Aria { get; set; }

        //public string AttributeDetail { get; set; }
        //public int AttributeDetailId { get; set; }

        //public string JobType { get; set; }
        //public int JobTypeId { get; set; }
    }
}
