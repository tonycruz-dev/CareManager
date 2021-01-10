using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class AgencyWithJobLocationSpecification : BaseSpecification<Agency>
    {
        public AgencyWithJobLocationSpecification()
        {
            AddInclude(x => x.ClientLocations);
        }
    }
}
