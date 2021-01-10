using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EntityHelpers
{
    public class CandidateBooked
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AccoutNumber { get; set; }
        public string AccoutName { get; set; }
        public string SortCode { get; set; }
        public string PhotoUrl { get; set; }

        public string AppUserId { get; set; }
        public string AppUser { get; set; }

        public string Grade { get; set; }
        public int GradeId { get; set; }
        public int JobToRequestId { get; set; }
        public int JobConfirmedId { get; set; }
        public int Raiting { get; set; }
        public bool FinishShift { get; set; }
        public bool LostShift { get; set; }
        public string Comment { get; set; }
    }
}
