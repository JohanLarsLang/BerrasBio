using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerrasBio.Models
{
    public class ShowingSeat
    {
        public int ID { get; set; }
        public int ShowingID { get; set; }
        public int Seat { get; set; }
        public bool Booked { get; set; }

        public Showing Showing { get; set; }
        
    }
}
