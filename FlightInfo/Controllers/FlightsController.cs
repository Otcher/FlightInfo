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
    public class FlightsController : BaseController
    {
        private readonly FlightInfoContext _context;

        public FlightsController(FlightInfoContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
            ViewData["IsAdmin"] = IsAdmin();

            return View(await _context.Flight.ToListAsync());
=======
            var planes = await _context.Flight
                .Include(f => f.Plane)
                .Include(f => f.Destination)
                .Include(f => f.Origin)
                .Include(f => f.Plane)
                .ToListAsync();

            return View(planes);
>>>>>>> 08e8e4e (Add flight views)
        }

        // GET: Flights/Details/5
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

            var flight = await _context.Flight
                    .Include(f => f.Plane)
                    .Include(f => f.Destination)
                    .Include(f => f.Origin)
                    .Include(f => f.Plane)
                    .Include(f => f.Pilot)
                    .Include(f => f.PassengerManifest)
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public async Task<ViewResult> Create()
        {
<<<<<<< HEAD
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

=======
            ViewBag.Airports = new SelectList(_context.Airport, "Id", "Name");
            ViewBag.Pilots = new SelectList(_context.Pilot, "Id", "FullName");
            ViewBag.Planes = new SelectList(_context.Plane, "Id", "Model");
            ViewBag.Passengers = new SelectList(_context.Passenger, "Id", "FullName");
>>>>>>> 08e8e4e (Add flight views)
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightNumber,DepartureTime")] Flight flight, int Origin, int Destination, int Pilot, int Plane, int[] PassengerManifest)
        {
            flight.Origin = _context.Airport.First(p => p.Id == Origin);
            flight.Destination = _context.Airport.First(p => p.Id == Destination);
            flight.Pilot = _context.Pilot.First(p => p.Id == Pilot);
            flight.Plane = _context.Plane.First(p => p.Id == Plane);
            
            flight.PassengerManifest ??= new List<Passenger>();
            flight.PassengerManifest.AddRange(_context.Passenger
                .Where(p => PassengerManifest.Contains(p.Id))
                .ToList());

            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
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

            var flight = await _context.Flight
                .Include(f => f.Plane)
                .Include(f => f.Destination)
                .Include(f => f.Origin)
                .Include(f => f.Plane)
                .Include(f => f.Pilot)
                .Include(f => f.PassengerManifest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            ViewBag.Airports = new SelectList(_context.Airport, "Id", "Name");
            ViewBag.Pilots = new SelectList(_context.Pilot, "Id", "FullName");
            ViewBag.Planes = new SelectList(_context.Plane, "Id", "Model");
            ViewBag.Passengers = await _context.Passenger.ToListAsync();

            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightNumber,DepartureTime")] Flight flight, int Origin, int Destination, int Pilot, int Plane, int[] PassengerManifest)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            flight.Origin = _context.Airport.First(p => p.Id == Origin);
            flight.Destination = _context.Airport.First(p => p.Id == Destination);
            flight.Pilot = _context.Pilot.First(p => p.Id == Pilot);
            flight.Plane = _context.Plane.First(p => p.Id == Plane);

            flight.PassengerManifest ??= new List<Passenger>();
            flight.PassengerManifest?.Clear();
            flight.PassengerManifest.AddRange(_context.Passenger
                .Where(p => PassengerManifest.Contains(p.Id))
                .ToList());

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
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
            return View(flight);
        }

        // GET: Flights/Delete/5
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

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flight.FindAsync(id);
            _context.Flight.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flight.Any(e => e.Id == id);
        }
    }
}
