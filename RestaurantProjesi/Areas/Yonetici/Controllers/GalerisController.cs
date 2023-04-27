using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantProjesi.Data;
using RestaurantProjesi.Models;

namespace RestaurantProjesi.Areas.Yonetici.Controllers
{
	[Area("Yonetici")]
	[Authorize]
	public class GalerisController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _whe;

		public GalerisController(ApplicationDbContext context, IWebHostEnvironment whe)
		{
			_context = context;
			_whe = whe;
		}

		// GET: Yonetici/Galeris
		public async Task<IActionResult> Index()
		{
			return View(await _context.Galeriler.ToListAsync());
		}

		// GET: Yonetici/Galeris/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var galeri = await _context.Galeriler
				.FirstOrDefaultAsync(m => m.Id == id);
			if (galeri == null)
			{
				return NotFound();
			}

			return View(galeri);
		}

		// GET: Yonetici/Galeris/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Yonetici/Galeris/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Galeri galeri)
		{
			if (ModelState.IsValid)
			{
				var files = HttpContext.Request.Form.Files;
				//IF DOSYA KONTROLÜ YAPTIM
				if (files.Count > 0)
				{
					var fileName = Guid.NewGuid().ToString();
					//RESİM EKLEMEK İÇİN PATH METODU KULLANILIR
					//RESMİ KAYDETMEK İSTEDİĞİM DOSYA YOLUNU BELİRTTİM
					var uploads = Path.Combine(_whe.WebRootPath, @"Website\menu");
					var extn = Path.GetExtension(files[0].FileName);

					if (galeri.Image != null)
					{
						var ImagePath = Path.Combine(_whe.WebRootPath, galeri.Image.TrimStart('\\'));
						if (System.IO.File.Exists(ImagePath))
						{
							System.IO.File.Delete(ImagePath);
						}
					}
					using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extn), FileMode.Create))
					{
						files[0].CopyTo(filesStreams);
					}
					galeri.Image = @"\Website\menu\" + fileName + extn;
				}
				_context.Add(galeri);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(galeri);
		}

		// GET: Yonetici/Galeris/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var galeri = await _context.Galeriler.FindAsync(id);
			if (galeri == null)
			{
				return NotFound();
			}
			return View(galeri);
		}

		// POST: Yonetici/Galeris/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] Galeri galeri)
		{
			if (id != galeri.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(galeri);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GaleriExists(galeri.Id))
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
			return View(galeri);
		}

		// GET: Yonetici/Galeris/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var galeri = await _context.Galeriler
				.FirstOrDefaultAsync(m => m.Id == id);
			if (galeri == null)
			{
				return NotFound();
			}

			return View(galeri);
		}

		// POST: Yonetici/Galeris/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var galeri = await _context.Galeriler.FindAsync(id);
			
			if (galeri.Image != null)
			{
				var ImagePath = Path.Combine(_whe.WebRootPath, galeri.Image.TrimStart('\\'));
				if (System.IO.File.Exists(ImagePath))
				{
					System.IO.File.Delete(ImagePath);
				}
			}
			_context.Galeriler.Remove(galeri);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GaleriExists(int id)
		{
			return _context.Galeriler.Any(e => e.Id == id);
		}
	}
}
