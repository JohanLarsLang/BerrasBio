using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerrasBio.Models
{
    public class Lounge
    {
        public int ID { get; set; }
        public string Info { get; set; }
        public int NrOfSeat { get; set; }
        public double TicketFee { get; set; }

        public ICollection<Showing> Showings { get; set; }
    }
}
