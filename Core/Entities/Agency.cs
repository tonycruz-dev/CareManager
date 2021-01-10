using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Agency : BaseEntity
    {
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
        public AppUser AppUser { get; set; }
        public IReadOnlyList<ClientLocation> ClientLocations { get; set; }
        public virtual IReadOnlyList<AgencyPhoto> AgencyPhotos { get; set; }
        public virtual IReadOnlyList<AgencyDocument> AgencyDocuments { get; set; }
    }
}
