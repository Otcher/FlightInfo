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
    public class PlanesController : BaseController
    {
        private readonly FlightInfoContext _context;

        public PlanesController(FlightInfoContext context)
        {
            _context = context;
        }

        // GET: Planes
        public async Task<IActionResult> Index(string SearchStringManufacturer, string SearchStringModel, int? SearchStringCapacity, int? SearchStringCruiseSpeed)
        {
            ViewData["IsAdmin"] = IsAdmin();

            ViewData["ManufacturerFilter"] = SearchStringManufacturer;
            ViewData["ModelFilter"] = SearchStringModel;
            ViewData["CapacityFilter"] = SearchStringCapacity;
            ViewData["CruiseSpeedFilter"] = SearchStringCruiseSpeed;

            var planes = from p in _context.Plane
                         select p;
            if (!String.IsNullOrEmpty(SearchStringManufacturer))
            {
                planes = planes.Where(p => p.Manufacturer.Contains(SearchStringManufacturer));
            }
            if (!String.IsNullOrEmpty(SearchStringModel))
            {
                planes = planes.Where(p => p.Model.Contains(SearchStringModel));
            }
            if (SearchStringCapacity.HasValue)
            {
                planes = planes.Where(p => p.Capacity.ToString().Contains(SearchStringCapacity.ToString()));
            }
            if (SearchStringCruiseSpeed.HasValue)
            {
                planes = planes.Where(p => p.CruiseSpeed.ToString().Contains(SearchStringCruiseSpeed.ToString()));
            }
            return View(await planes.ToListAsync());
        }

        // GET: Planes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _context.Plane
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        // GET: Planes/Create
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Planes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Capacity,CruiseSpeed")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plane);
        }

        // GET: Planes/Edit/5
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

            var plane = await _context.Plane.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            return View(plane);
        }

        // POST: Planes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,Capacity,CruiseSpeed")] Plane plane)
        {
            if (id != plane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaneExists(plane.Id))
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
            return View(plane);
        }

        // GET: Planes/Delete/5
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

            var plane = await _context.Plane
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        // POST: Planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plane = await _context.Plane.FindAsync(id);
            _context.Plane.Remove(plane);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaneExists(int id)
        {
            return _context.Plane.Any(e => e.Id == id);
        }
    }
}
