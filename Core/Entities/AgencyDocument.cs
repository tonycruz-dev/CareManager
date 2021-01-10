using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
   public class AgencyDocument: BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public virtual Agency Agency { get; set; }
        public int AgencyId { get; set; }
    }
}
