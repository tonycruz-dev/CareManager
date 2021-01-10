using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EntityHelpers
{
    public class AgencyListLoop
    {
        public IReadOnlyList<TimeDetail> TimeDetails { get; set; }
        public IReadOnlyList<JobType> JobTypes { get; set; }
        public IReadOnlyList<Grade> Grades { get; set; }

        public IReadOnlyList<ClientLocation> ClientLocations { get; set; }
        public IReadOnlyList<AttributeDetail> AttributeDetails { get; set; }

        public IReadOnlyList<ShiftState> ShiftStates { get; set; }
        public IReadOnlyList<PaymentType> PaymentTypes { get; set; }
        public IReadOnlyList<Aria> Arias { get; set; }
    }
}
