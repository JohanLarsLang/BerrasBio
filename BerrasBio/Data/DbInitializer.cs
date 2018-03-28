using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerrasBio.Models;

namespace BerrasBio.Data
{
    public class DbInitializer
    {
        public static void Initialize(CinemaContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }

            var movies = new Movie[]
            {
            new Movie{Title="Star Wars - The End War",Info="The ultimate Star Wars end film", TimeSpan = new TimeSpan(3, 15, 0), TicketPrice=110},
            new Movie{Title="Lord of Rings - The Story Continue",Info="The continue story of the rings", TimeSpan = new TimeSpan(3, 06, 0), TicketPrice=120},
            new Movie{Title="Matrix - The lost pill",Info="The Matrix is back with a new adventure", TimeSpan = new TimeSpan(2, 47, 0), TicketPrice=130}

            };
            foreach (Movie m in movies)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var lounges = new Lounge[]
            {
            new Lounge{Info="The small lounge with 3D screen and comfortable chairs",NrOfSeat=50, TicketFee=1},
            new Lounge{Info="The big lounge with 3D screen and simolator comfortable chairs for optional 4D enjoyment", NrOfSeat=100, TicketFee=1.1}
        
            };
            foreach (Lounge l in lounges)
            {
                context.Lounges.Add(l);
            }
            context.SaveChanges();

            var showings= new Showing[]
            {
            new Showing{StartTime=DateTime.Parse("16:30"),MovieID=1,LoungeID=1,SeatsLeft=5},
            new Showing{StartTime=DateTime.Parse("20:00"),MovieID=2,LoungeID=1,SeatsLeft=5},
            new Showing{StartTime=DateTime.Parse("23:30"),MovieID=3,LoungeID=1,SeatsLeft=5},
             new Showing{StartTime=DateTime.Parse("16:00"),MovieID=1,LoungeID=2,SeatsLeft=5},
            new Showing{StartTime=DateTime.Parse("19:30"),MovieID=2,LoungeID=2,SeatsLeft=5},
            new Showing{StartTime=DateTime.Parse("23:00"),MovieID=3,LoungeID=2,SeatsLeft=15}

            };
            foreach (Showing s in showings)
            {
                context.Showings.Add(s);
            }
            context.SaveChanges();

            /*
            var showingseats = new ShowingSeat[]
   {
            new ShowingSeat{ID=1,ShowingID=1,Seat=1,Booked=false},
            new ShowingSeat{ID=2,ShowingID=1,Seat=2,Booked=false},
            new ShowingSeat{ID=3,ShowingID=1,Seat=3,Booked=false},
            new ShowingSeat{ID=4,ShowingID=1,Seat=4,Booked=false},
            new ShowingSeat{ID=5,ShowingID=1,Seat=5,Booked=false},
            new ShowingSeat{ID=6,ShowingID=2,Seat=1,Booked=false},
            new ShowingSeat{ID=7,ShowingID=2,Seat=2,Booked=false},
            new ShowingSeat{ID=8,ShowingID=2,Seat=3,Booked=false},
            new ShowingSeat{ID=9,ShowingID=2,Seat=4,Booked=false},
            new ShowingSeat{ID=10,ShowingID=2,Seat=5,Booked=false},
            new ShowingSeat{ID=11,ShowingID=3,Seat=1,Booked=false},
            new ShowingSeat{ID=12,ShowingID=3,Seat=2,Booked=false},
            new ShowingSeat{ID=13,ShowingID=3,Seat=3,Booked=false},
            new ShowingSeat{ID=14,ShowingID=3,Seat=4,Booked=false},
            new ShowingSeat{ID=15,ShowingID=3,Seat=5,Booked=false},
            new ShowingSeat{ID=16,ShowingID=4,Seat=1,Booked=false},
            new ShowingSeat{ID=17,ShowingID=4,Seat=2,Booked=false},
            new ShowingSeat{ID=18,ShowingID=4,Seat=3,Booked=false},
            new ShowingSeat{ID=19,ShowingID=4,Seat=4,Booked=false},
            new ShowingSeat{ID=20,ShowingID=4,Seat=5,Booked=false},
            new ShowingSeat{ID=21,ShowingID=5,Seat=1,Booked=false},
            new ShowingSeat{ID=22,ShowingID=5,Seat=2,Booked=false},
            new ShowingSeat{ID=23,ShowingID=5,Seat=3,Booked=false},
            new ShowingSeat{ID=24,ShowingID=5,Seat=4,Booked=false},
            new ShowingSeat{ID=25,ShowingID=5,Seat=5,Booked=false},
            new ShowingSeat{ID=26,ShowingID=6,Seat=1,Booked=false},
            new ShowingSeat{ID=27,ShowingID=6,Seat=2,Booked=false},
            new ShowingSeat{ID=28,ShowingID=6,Seat=3,Booked=false},
            new ShowingSeat{ID=29,ShowingID=6,Seat=4,Booked=false},
            new ShowingSeat{ID=30,ShowingID=6,Seat=5,Booked=false},
            new ShowingSeat{ID=31,ShowingID=6,Seat=6,Booked=false},
            new ShowingSeat{ID=32,ShowingID=6,Seat=7,Booked=false},
            new ShowingSeat{ID=33,ShowingID=6,Seat=8,Booked=false},
            new ShowingSeat{ID=34,ShowingID=6,Seat=9,Booked=false},
            new ShowingSeat{ID=35,ShowingID=6,Seat=10,Booked=false},
            new ShowingSeat{ID=36,ShowingID=6,Seat=11,Booked=false},
            new ShowingSeat{ID=37,ShowingID=6,Seat=12,Booked=false},
            new ShowingSeat{ID=38,ShowingID=6,Seat=13,Booked=false},
            new ShowingSeat{ID=39,ShowingID=6,Seat=14,Booked=false},
            new ShowingSeat{ID=40,ShowingID=6,Seat=15,Booked=false}

        

   };
            foreach (ShowingSeat ss in showingseats)
            {
                context.ShowingSeats.Add(ss);
            }
            context.SaveChanges();
            */
        }
    
}
}

