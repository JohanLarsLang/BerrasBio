using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;

namespace BerrasBio.Controllers
{
    public class ShowingsController : Controller
    {
        private readonly CinemaContext _context;

        public ShowingsController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["StartSortParm"] = sortOrder == "Time" ? "time_desc" : "Time";
            ViewData["MovieTitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "movietitle_desc" : "";
            ViewData["TimespanSortParm"] = sortOrder == "Timespan" ? "timespan_desc" : "Timespan";
            ViewData["LoungeNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "loungename_desc" : "";
            ViewData["SeatsLeftSortParm"] = sortOrder == "SeatsLeft" ? "seatleft_desc" : "SeatsLeft";

            DateTime today = DateTime.Today;
            ViewData["DateToDay"] = today.ToString("yyyy-MM-dd");

            var showings = from s in _context.Showings
                           .Include(m => m.Movie)
                           .Include(l => l.Lounge)
                           select s;

            switch (sortOrder)
            {
                case "Time":
                    showings = showings.OrderBy(s => s.StartTime);
                    break;

                case "time_desc":
                    showings = showings.OrderByDescending(s => s.StartTime);
                    break;

                case "movietitle_desc":
                    showings = showings.OrderBy(m => m.Movie.Title);
                    break;

                case "Timespan":
                    showings = showings.OrderBy(s => s.Movie.TimeSpan);
                    break;

                case "timespan_desc":
                    showings = showings.OrderByDescending(s => s.Movie.TimeSpan);
                    break;

                case "loungename_desc":
                    showings = showings.OrderBy(s => s.LoungeID);
                    break;

                case "SeatsLeft":
                    showings = showings.OrderByDescending(s => s.SeatsLeft);
                    break;

                case "seatsleft_desc":
                    showings = showings.OrderByDescending(s => s.SeatsLeft);
                    break;

                default:
                    showings = showings.OrderBy(s => s.StartTime);
                    break;

            }


            return View(await showings.AsNoTracking().ToListAsync());

        }

        // GET: Showings/Book/4
        [HttpGet]
        public ActionResult Book(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedShowing = _context.Showings
           .Include(a => a.Lounge)
           .Include(m => m.Movie)
           .SingleOrDefault(m => m.ID == id);

            DateTime today = DateTime.Today;
            ViewData["DateToDay"] = today.ToString("yyyy-MM-dd");

            ViewData["SeatsLeft"] = selectedShowing.SeatsLeft < 12 ? selectedShowing.SeatsLeft : 12;

            ViewData["BookAvailable"] = selectedShowing.SeatsLeft;

            var ticketMoviePrice = selectedShowing.Movie.TicketPrice;

            var loungeTicketFee = (decimal)selectedShowing.Lounge.TicketFee;

            ViewData["TicketPrice"] = ticketMoviePrice * loungeTicketFee;

            if (selectedShowing == null)
            {
                return NotFound();
            }

            return View(selectedShowing);
        }

        // POST: Showings/Book/4
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Book(int? id, int nrOfTickets)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedShowing = _context.Showings
               .Include(a => a.Lounge)
               .Include(m => m.Movie)
               .SingleOrDefault(m => m.ID == id);

             bookedShowing.SeatsLeft -= nrOfTickets;

            _context.Update(bookedShowing);
            _context.SaveChanges();

            if (bookedShowing == null)
            {
                return NotFound();
            }

            return RedirectToAction("Booked", new {id, nrOfTickets });

            //return RedirectToAction(nameof(Booked), nameof(ShowingsController), ""+id +"/"+nrOfTickets);
            //    /Showings/Booked#5/2

            //return View(bookedShowing);
        }

        [HttpGet]
        public ActionResult Booked(int? id, int? nrOfTickets)
        {
            if (id == null || nrOfTickets == null)
            {
                return NotFound();
            }

            var selectedShowing = _context.Showings
           .Include(a => a.Lounge)
           .Include(m => m.Movie)
           .SingleOrDefault(m => m.ID == id);

            DateTime today = DateTime.Today;
            ViewData["DateToDay"] = today.ToString("yyyy-MM-dd");

            ViewData["NrOfTIckets"] = nrOfTickets;

            var ticketMoviePrice = selectedShowing.Movie.TicketPrice;

            var loungeTicketFee = (decimal)selectedShowing.Lounge.TicketFee;

            ViewData["TicketPrice"] = ticketMoviePrice * loungeTicketFee;

            ViewData["TotalTicketPrice"] = ticketMoviePrice * loungeTicketFee * nrOfTickets;


            if (selectedShowing == null)
            {
                return NotFound();
            }

            return View(selectedShowing);
        }

        /*
        // GET: Showings/Book/4
        public async Task<IActionResult> Book(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DateTime today = DateTime.Today;
            ViewData["DateToDay"] = today.ToString("yyyy-MM-dd");

            //var showing = await _context.Showings.SingleOrDefaultAsync(m => m.ID == id);

            var showing = await _context.Showings
              .Include(s => s.Lounge)
              .Include(s => s.Movie)
              .SingleOrDefaultAsync(m => m.ID == id);

            if (showing == null)
            {
                return NotFound();
            }
            ViewData["LoungeID"] = new SelectList(_context.Lounges, "ID", "ID", showing.LoungeID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", showing.MovieID);
    
           return View(showing);
        }

  


        

        /*
          public async Task<IActionResult> Index(int? id)
          {
              if (id == null)
              {
                  return NotFound();
              }

              var showings = await _context.Showings
                  .Include(s => s.Movie)
                      .Include(e => e.Lounge)
                  .AsNoTracking()
                  .SingleOrDefaultAsync(m => m.ID == id);

              if (showings == null)
              {
                  return NotFound();
              }

              return View(showings);
          }


          // GET: Showings
          public async Task<IActionResult> Index()
          {
              var cinemaContext = _context.Showings.Include(s => s.Lounge).Include(s => s.Movie);
              return View(await cinemaContext.ToListAsync());
          }

          */
        // GET: Showings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showings
                .Include(s => s.Lounge)
                .Include(s => s.Movie)
                .SingleOrDefaultAsync(m => m.ID == id);

            var ticketMoviePrice = showing.Movie.TicketPrice;

            var loungeTicketFee = (decimal)showing.Lounge.TicketFee;

            ViewData["TicketPrice"] = ticketMoviePrice * loungeTicketFee;

            DateTime today = DateTime.Today;
            ViewData["DateToDay"] = today.ToString("yyyy-MM-dd");

            if (showing == null)
            {
                return NotFound();
            }

            return View(showing);
        }

        // GET: Showings/Create
        public IActionResult Create()
        {
            ViewData["LoungeID"] = new SelectList(_context.Lounges, "ID", "ID");
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID");
            return View();
        }

        // POST: Showings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartTime,MovieID,LoungeID,SeatsLeft")] Showing showing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoungeID"] = new SelectList(_context.Lounges, "ID", "ID", showing.LoungeID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", showing.MovieID);
            return View(showing);
        }

        // GET: Showings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showings.SingleOrDefaultAsync(m => m.ID == id);
            if (showing == null)
            {
                return NotFound();
            }
            ViewData["LoungeID"] = new SelectList(_context.Lounges, "ID", "ID", showing.LoungeID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", showing.MovieID);
            return View(showing);
        }

        // POST: Showings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartTime,MovieID,LoungeID,SeatsLeft")] Showing showing)
        {
            if (id != showing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowingExists(showing.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoungeID"] = new SelectList(_context.Lounges, "ID", "ID", showing.LoungeID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", showing.MovieID);
            return View(showing);
        }

        // GET: Showings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showings
                .Include(s => s.Lounge)
                .Include(s => s.Movie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (showing == null)
            {
                return NotFound();
            }

            return View(showing);
        }

        // POST: Showings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showing = await _context.Showings.SingleOrDefaultAsync(m => m.ID == id);
            _context.Showings.Remove(showing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowingExists(int id)
        {
            return _context.Showings.Any(e => e.ID == id);
        }
    }
}
