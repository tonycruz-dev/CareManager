using System.Collections.Generic;

namespace Core.Entities
{
    public class Candidate: BaseEntity
    {
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
        public AppUser AppUser { get; set; }

        public Grade Grade { get; set; }
        public int GradeId { get; set; }

        public IReadOnlyList<InvitedCandidate> InvitedCandidates { get; set; }
        public IReadOnlyList<JobConfirmed> JobConfirmeds { get; set; }
        public virtual IReadOnlyList<CandidatePhoto> CandidatePhotos { get; set; }
        public virtual IReadOnlyList<CandidateDocument> CandidateDocuments { get; set; }
    }
}
