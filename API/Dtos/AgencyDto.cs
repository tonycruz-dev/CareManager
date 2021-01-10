using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AgencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
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
        public string LogoUrl { get; set; }
        public string AppUserId { get; set; }
        public string AppUser { get; set; }

    }
}
