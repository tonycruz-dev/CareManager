using System;

namespace Core.Entities
{
    public class AgencyPhoto: BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public virtual Agency Agency { get; set; }
        public int AgencyId { get; set; }
    }
}
