using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BerrasBio.Models
{
    public class Showing
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        public int MovieID { get; set; }
        public int LoungeID { get; set; }
        public int SeatsLeft { get; set; }
        public Movie Movie { get; set; }
        public Lounge Lounge { get; set; }

        public ICollection<ShowingSeat> ShowingSeats { get; set; }
    }
}
