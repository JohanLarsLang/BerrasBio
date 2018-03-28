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
    public class ShowingSeatsController : Controller
    {
        private readonly CinemaContext _context;

        public ShowingSeatsController(CinemaContext context)
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

            var showingseats = from s in _context.ShowingSeats.Include(s => s.Showing).ThenInclude(m => m.Movie)
                               select s;

            switch (sortOrder)
            {
                case "Time":
                    showingseats = showingseats.OrderBy(s => s.Showing.StartTime);
                    break;

                case "time_desc":
                    showingseats = showingseats.OrderByDescending(s => s.Showing.StartTime);
                    break;

                case "movietitle_desc":
                    showingseats = showingseats.OrderBy(m => m.Showing.Movie.Title);
                    break;

                case "Timespan":
                    showingseats = showingseats.OrderBy(s => s.Showing.Movie.TimeSpan);
                    break;

                case "timespan_desc":
                    showingseats = showingseats.OrderByDescending(s => s.Showing.Movie.TimeSpan);
                    break;

                case "loungename_desc":
                    showingseats = showingseats.OrderBy(s => s.Showing.LoungeID);
                    break;

                case "SeatsLeft":
                    showingseats = showingseats.OrderByDescending(s => s.Showing.SeatsLeft);
                    break;

                case "seatsleft_desc":
                    showingseats = showingseats.OrderByDescending(s => s.Showing.SeatsLeft);
                    break;

                default:
                    showingseats = showingseats.OrderBy(s => s.Showing.StartTime);
                    break;

            }


            return View(await showingseats.AsNoTracking().ToListAsync());

        }

        /*
        // GET: ShowingSeats
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.ShowingSeats.Include(s => s.Showing);
            return View(await cinemaContext.ToListAsync());
        }
        */

        // GET: ShowingSeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showingSeat = await _context.ShowingSeats
                .Include(s => s.Showing)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (showingSeat == null)
            {
                return NotFound();
            }

            return View(showingSeat);
        }

        // GET: ShowingSeats/Create
        public IActionResult Create()
        {
            ViewData["ShowingID"] = new SelectList(_context.Showings, "ID", "ID");
            return View();
        }

        // POST: ShowingSeats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ShowingID,Seat,Booked")] ShowingSeat showingSeat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showingSeat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShowingID"] = new SelectList(_context.Showings, "ID", "ID", showingSeat.ShowingID);
            return View(showingSeat);
        }

        // GET: ShowingSeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showingSeat = await _context.ShowingSeats.SingleOrDefaultAsync(m => m.ID == id);
            if (showingSeat == null)
            {
                return NotFound();
            }
            ViewData["ShowingID"] = new SelectList(_context.Showings, "ID", "ID", showingSeat.ShowingID);
            return View(showingSeat);
        }

        // POST: ShowingSeats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ShowingID,Seat,Booked")] ShowingSeat showingSeat)
        {
            if (id != showingSeat.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showingSeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowingSeatExists(showingSeat.ID))
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
            ViewData["ShowingID"] = new SelectList(_context.Showings, "ID", "ID", showingSeat.ShowingID);
            return View(showingSeat);
        }

        // GET: ShowingSeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showingSeat = await _context.ShowingSeats
                .Include(s => s.Showing)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (showingSeat == null)
            {
                return NotFound();
            }

            return View(showingSeat);
        }

        // POST: ShowingSeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showingSeat = await _context.ShowingSeats.SingleOrDefaultAsync(m => m.ID == id);
            _context.ShowingSeats.Remove(showingSeat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowingSeatExists(int id)
        {
            return _context.ShowingSeats.Any(e => e.ID == id);
        }
    }
}
