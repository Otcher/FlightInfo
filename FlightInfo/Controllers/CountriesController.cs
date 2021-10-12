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
    public class CountriesController : BaseController
    {
        private readonly FlightInfoContext _context;

        public CountriesController(FlightInfoContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index(string searchStringName)
        {
            ViewData["IsAdmin"] = IsAdmin();

            ViewData["NameFilter"] = searchStringName;

            var countries = from c in _context.Country
                           select c;
            if (!String.IsNullOrEmpty(searchStringName))
            {
                countries = countries.Where(c => c.Name.Contains(searchStringName));
            }
            return View(await countries.ToListAsync());
        }

        // GET: Countries/Details/5
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

            var country = await _context.Country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
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

            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
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

            var country = await _context.Country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Country.FindAsync(id);
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.Id == id);
        }

        // GET: Countries/AirpotsInCountries
        public IActionResult AirpotsInCountries()
        {
            var AirpotsInCountries = from a in _context.Airport
                                    join c in _context.City
                                    on a.CityId equals c.Id
                                    group a by c.Country.Name into g
                                    select new { CountryName = g.Key, AirportsCount = g.Count() };

            return Json(AirpotsInCountries.ToList());
        }

        // GET: Countries/FlightsToCountries
        public IActionResult FlightsToCountries()
        {
            var AirpotsInCountries = from f in _context.Flight
                                     join c in _context.City
                                     on f.Destination.CityId equals c.Id
                                     group f by c.Country.Name into g
                                     select new { CountryName = g.Key, FlightsCount = g.Count() };

            return Json(AirpotsInCountries.ToList());
        }
    }
}
