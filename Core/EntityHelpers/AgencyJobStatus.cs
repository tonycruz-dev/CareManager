using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EntityHelpers
{
    public class AgencyJobStatus
    {
        public int Booked { get; set; }
        public int Canceled { get; set; }
        public int Finish { get; set; }
        public int InProgress { get; set; }
        public int Pending { get; set; }

    }
}
