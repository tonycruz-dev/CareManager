using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class CandidatePhoto: BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }
}
