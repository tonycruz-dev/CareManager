using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CandidateForInvitationDto
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
        public string PhotoUrl { get; set; }
        public string AppUserId { get; set; }
        public string Grade { get; set; }
        public int GradeId { get; set; }
        public bool isSelected { get; set; }
    }
    public class CandidateRespondedDto
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
        public string PhotoUrl { get; set; }
        public string AppUserId { get; set; }
        public string Grade { get; set; }
        public int GradeId { get; set; }
        public bool? Accept { get; set; }
        public bool? Reject { get; set; }
        public bool isSelected { get; set; }
    }
}
