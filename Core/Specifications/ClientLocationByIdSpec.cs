using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ClientLocationByIdSpec : BaseSpecification<ClientLocation>
    {
        public ClientLocationByIdSpec(int clientId) : base(c => c.Id == clientId)
        {

        }
    }
}
