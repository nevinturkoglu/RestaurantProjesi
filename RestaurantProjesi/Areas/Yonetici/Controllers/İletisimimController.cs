using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantProjesi.Data;
using RestaurantProjesi.Models;

namespace RestaurantProjesi.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
	[Authorize]
	public class İletisimimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public İletisimimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yonetici/İletisimim
        public async Task<IActionResult> Index()
        {
            return View(await _context.İletisimimler.ToListAsync());
        }

        // GET: Yonetici/İletisimim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisimim = await _context.İletisimimler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (İletisimim == null)
            {
                return NotFound();
            }

            return View(İletisimim);
        }

        // GET: Yonetici/İletisimim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/İletisimim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Telefon,Adres")] İletisimim İletisimim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(İletisimim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(İletisimim);
        }

        // GET: Yonetici/İletisimim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisimim = await _context.İletisimimler.FindAsync(id);
            if (İletisimim == null)
            {
                return NotFound();
            }
            return View(İletisimim);
        }

        // POST: Yonetici/İletisimim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Telefon,Adres")] İletisimim İletisimim)
        {
            if (id != İletisimim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(İletisimim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!İletisimimExists(İletisimim.Id))
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
            return View(İletisimim);
        }

        // GET: Yonetici/İletisimim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var İletisimim = await _context.İletisimimler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (İletisimim == null)
            {
                return NotFound();
            }

            return View(İletisimim);
        }

        // POST: Yonetici/İletisimim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var İletisimim = await _context.İletisimimler.FindAsync(id);
            _context.İletisimimler.Remove(İletisimim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool İletisimimExists(int id)
        {
            return _context.İletisimimler.Any(e => e.Id == id);
        }
    }
}
