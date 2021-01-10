using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class HRStaff : BaseEntity
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
        public string PhotoUrl { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
