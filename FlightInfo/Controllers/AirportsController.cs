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
    public class AirportsController : BaseController
    {
        private readonly FlightInfoContext _context;

        public AirportsController(FlightInfoContext context)
        {
            _context = context;
        }

        // GET: Airports
        public async Task<IActionResult> Index(string SearchStringName, string SearchStringCity, double? SearchStringLatitude, double? SearchStringLongtitude)
        {
            ViewData["IsAdmin"] = IsAdmin();

            ViewData["NameFilter"] = SearchStringName;
            ViewData["CityFilter"] = SearchStringCity;
            ViewData["LatitudeFilter"] = SearchStringLatitude;
            ViewData["LongtitudeFilter"] = SearchStringLongtitude;

            var airports = from a in _context.Airport.Include(a => a.City)
                         select a;
            if (!String.IsNullOrEmpty(SearchStringName))
            {
                airports = airports.Where(a => a.Name.Contains(SearchStringName));
            }
            if (!String.IsNullOrEmpty(SearchStringCity))
            {
                airports = airports.Where(a => a.City.Name.Contains(SearchStringCity));
            }
            if (SearchStringLatitude.HasValue)
            {
                airports = airports.Where(a => a.Latitude.ToString().Contains(SearchStringLatitude.ToString()));
            }
            if (SearchStringLongtitude.HasValue)
            {
                airports = airports.Where(a => a.Longtitude.ToString().Contains(SearchStringLongtitude.ToString()));
            }

            return View(await airports.ToListAsync());
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            IsAdmin();

            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airport
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Cities"] = new SelectList(_context.City.Where(c => c.Airport == null), "Id", "Name");

            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Latitude,Longtitude")] Airport airport, int City)
        {
            airport.City = _context.City.First(c => c.Id == City);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(airport);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return Content("Unable to create");
                }
                
            }
            ViewData["Id"] = new SelectList(_context.City, "Id", "Id", airport.Id);
            return View(airport);
        }

        // GET: Airports/Edit/5
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

            var airport = await _context.Airport.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            ViewBag.Cities = new SelectList(_context.City.Where(c => c.Airport == null || c.Id == airport.CityId), "Id", "Name", airport.CityId);
            return View(airport);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Latitude,Longtitude")] Airport airport, int CityId)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            airport.CityId = CityId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.Id))
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
            ViewData["Id"] = new SelectList(_context.City, "Id", "Id", airport.Id);
            return View(airport);
        }

        // GET: Airports/Delete/5
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

            var airport = await _context.Airport
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await _context.Airport.FindAsync(id);
            _context.Airport.Remove(airport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportExists(int id)
        {
            return _context.Airport.Any(e => e.Id == id);
        }
    }
}
