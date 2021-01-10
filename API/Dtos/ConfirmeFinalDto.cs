using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ConfirmeFinalDto
    {
        public int JobToRequestId { get; set; }
        public int JobConfirmedId { get; set; }
        public int Raiting { get; set; }
        public string Comment { get; set; }
    }
}
