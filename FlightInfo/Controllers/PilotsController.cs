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
    public class PilotsController : BaseController
    {
        private readonly FlightInfoContext _context;

        public PilotsController(FlightInfoContext context)
        {
            _context = context;
        }

        // GET: Pilots
        public async Task<IActionResult> Index(string searchStringFirstName, string searchStringLastName)
        {
            ViewData["IsAdmin"] = IsAdmin();

            ViewData["FirstNameFilter"] = searchStringFirstName;
            ViewData["LastNameFilter"] = searchStringLastName;

            var pilots = from p in _context.Pilot
                         select p;
            if (!String.IsNullOrEmpty(searchStringFirstName))
            {
                pilots = pilots.Where(p => p.FirstName.Contains(searchStringFirstName));
            }
            if (!String.IsNullOrEmpty(searchStringLastName))
            {
                pilots = pilots.Where(p => p.LastName.Contains(searchStringLastName));
            }
            return View(await pilots.ToListAsync());
        }

        // GET: Pilots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            IsAdmin();

            if (id == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilot
                .Include(p => p.Qualification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        // GET: Pilots/Create
        public async Task<IActionResult> Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Planes = new SelectList(_context.Plane, "Id", "Model");
            return View();
        }

        // POST: Pilots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthdate")] Pilot pilot, List<int> Qualification)
        {
            if (Qualification == null)
            {
                return NotFound();
            }

            Qualification.ForEach(i =>
            {
                var plane = _context.Plane.First(p => p.Id == i);
                pilot.Qualification ??= new List<Plane>();
                pilot.Qualification.Add(plane);
            });

            if (ModelState.IsValid)
            {
                _context.Add(pilot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pilot);
        }

        // GET: Pilots/Edit/5
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

            var pilot = await _context.Pilot
                .Include(p => p.Qualification)
                .FirstAsync(p => p.Id == id);

            if (pilot == null)
            {
                return NotFound();
            }

            ViewBag.Planes = await _context.Plane.ToListAsync();

            return View(pilot);
        }

        // POST: Pilots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birthdate,Qualification")] Pilot pilot, int[] qualifications)
        {
            if (id != pilot.Id)
            {
                return NotFound();
            }

            var dbPilot = _context.Pilot.Include(p => p.Qualification).Single(p => p.Id == id);

            dbPilot.Qualification ??= new List<Plane>();
            dbPilot.Qualification.Clear();
            dbPilot.Qualification.AddRange(_context.Plane.Where(p => qualifications.Contains(p.Id)).ToList());

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotExists(pilot.Id))
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
            return View(pilot);
        }

        // GET: Pilots/Delete/5
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

            var pilot = await _context.Pilot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        // POST: Pilots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pilot = await _context.Pilot.FindAsync(id);
            _context.Pilot.Remove(pilot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilotExists(int id)
        {
            return _context.Pilot.Any(e => e.Id == id);
        }
    }
}
