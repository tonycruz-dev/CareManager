using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
  public class RetunAgencyLoop
    {
        public List<TimeDetail> TimeDetails { get; set; }
        public List<JobType> JobTypes { get; set; }
        public List<Grade> Grades { get; set; }

        public List<ClientLocation> ClientLocations { get; set; }
        public List<AttributeDetail> AttributeDetails { get; set; }

        public List<ShiftState> ShiftStates { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public List<Aria> Arias { get; set; }
    }
}
