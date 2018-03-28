using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BerrasBio.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public string Info { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan TimeSpan { get; set; }
        public decimal TicketPrice{ get; set; }

        public ICollection<Showing> Showings { get; set; }
    }
}
