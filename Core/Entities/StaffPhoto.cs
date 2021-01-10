using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class StaffPhoto : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int StaffId { get; set; }
    }
}
