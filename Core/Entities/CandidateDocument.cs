using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
   public class CandidateDocument : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }
}
