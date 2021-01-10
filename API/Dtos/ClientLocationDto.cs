using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ClientLocationDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ManagerName { get; set; }
        public string ContactName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string ContactNumber { get; set; }
        public int AgencyId { get; set; }
    }
}
