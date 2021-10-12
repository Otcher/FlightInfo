using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightInfo.Data;
using FlightInfo.Models;

namespace FlightInfo.Controllers
{
    public class PassengersController : BaseController
    {
        private readonly FlightInfoContext _context;

        public PassengersController(FlightInfoContext context)
        {
            _context = context;
        }

        // GET: Passengers
        public async Task<IActionResult> Index(string searchStringFirstName, string searchStringLastName)
        {
            ViewData["IsAdmin"] = IsAdmin();

            ViewData["FirstNameFilter"] = searchStringFirstName;
            ViewData["LastNameFilter"] = searchStringLastName;

            var passengers = from p in _context.Passenger
                         select p;
            if (!String.IsNullOrEmpty(searchStringFirstName))
            {
                passengers = passengers.Where(p => p.FirstName.Contains(searchStringFirstName));
            }
            if (!String.IsNullOrEmpty(searchStringLastName))
            {
                passengers = passengers.Where(p => p.LastName.Contains(searchStringLastName));
            }
            return View(await passengers.ToListAsync());
        }

        // GET: Passengers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passenger
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // GET: Passengers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthdate")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        // GET: Passengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passenger.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return View(passenger);
        }

        // POST: Passengers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birthdate")] Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.Id))
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
            return View(passenger);
        }

        // GET: Passengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passenger
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passenger.FindAsync(id);
            _context.Passenger.Remove(passenger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Passengers/PassengersByAgeGroup
        public IActionResult PassengersByAgeGroup()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var groupedAges = _context.Passenger
                .Where(p => p.Birthdate != null).GroupBy(p => EF.Functions.DateDiffDay(p.Birthdate, DateTime.Today) / 365 / 10)
                .Select(g => new { Age = g.Key, Count = g.Count() }).ToList();

            return Json(groupedAges);
        }

        private bool PassengerExists(int id)
        {
            return _context.Passenger.Any(e => e.Id == id);
        }
    }
}
