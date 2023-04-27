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
	public class HakkındasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HakkındasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yonetici/Hakkındas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hakkındalar.ToListAsync());
        }

        // GET: Yonetici/Hakkındas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hakkında = await _context.Hakkındalar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hakkında == null)
            {
                return NotFound();
            }

            return View(hakkında);
        }

        // GET: Yonetici/Hakkındas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/Hakkındas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Hakkında hakkında)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hakkında);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hakkında);
        }

        // GET: Yonetici/Hakkındas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hakkında = await _context.Hakkındalar.FindAsync(id);
            if (hakkında == null)
            {
                return NotFound();
            }
            return View(hakkında);
        }

        // POST: Yonetici/Hakkındas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Hakkında hakkında)
        {
            if (id != hakkında.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hakkında);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HakkındaExists(hakkında.Id))
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
            return View(hakkında);
        }

        // GET: Yonetici/Hakkındas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hakkında = await _context.Hakkındalar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hakkında == null)
            {
                return NotFound();
            }

            return View(hakkında);
        }

        // POST: Yonetici/Hakkındas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hakkında = await _context.Hakkındalar.FindAsync(id);
            _context.Hakkındalar.Remove(hakkında);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HakkındaExists(int id)
        {
            return _context.Hakkındalar.Any(e => e.Id == id);
        }
    }
}
