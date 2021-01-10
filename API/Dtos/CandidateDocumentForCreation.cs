using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CandidateDocumentForCreation
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public int CandidateId { get; set; }
        public CandidateDocumentForCreation()
        {
            DateAdded = DateTime.Now;
        }
    }
}
