using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class JobToRequestForInsertDto
    {
        public int Id { get; set; }
        public int NumberCandidate { get; set; }
        public DateTime[] DateRange { get; set; }
        public int TimeDetailId { get; set; }
        public int EndTimeDetailId { get; set; }
        public int JobTypeId { get; set; }
        public int AgencyId { get; set; }
        public int GradeId { get; set; }
        public int ClientLocationId { get; set; }
        public int AttributeDetailId { get; set; }
        public int PaymentTypeId { get; set; }
        public int AriaId { get; set; }
        public string AppUserId { get; set; }
       
    }
}
