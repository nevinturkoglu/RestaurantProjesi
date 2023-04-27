using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using RestaurantProjesi.Data;
using RestaurantProjesi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProjesi.Areas.Musteri.Controllers//namespace düzenleme ile homecontroller yolunu düzenledik
{
	[Area("Musteri")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger; //dependency injection yaptım-bağımlılık azalttım-her türlü teknolojiye uyum sağlar.
		private readonly ApplicationDbContext _db;
		//private readonly IToastNotification _toast;
		//private readonly IWebHostEnvironment _whe;
		private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _whe;


        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db,IToastNotification toast,IWebHostEnvironment whe)
		{
			_logger = logger;
			_db = db;
			_toast= toast;
			_whe= whe;	
		}

		public IActionResult Index()
		{
			//özel menüleri ana sayfada getirme işlemi
			var menu_=_db.Menuler.Where(i=>i.OzelMenu).ToList();
			return View(menu_);
		}

		public IActionResult CategoryDetails(int? id)
		{
			//kategorilere ait menüleri getirme
			var menu=_db.Menuler.Where(i=>i.CategoryId== id).ToList();
			ViewBag.KategoriId = id;
			return View(menu);
		}

		public IActionResult Menu()
		{
			//menü sayfasına tüm menüleri getirme işlemi
			var menu=_db.Menuler.ToList();
			return View(menu);
		}
      //yönetici tarafında yazılan kodlar buradaki action methoduyla müsteri tarafında da çekilmiş oluyor.
        public IActionResult Galeri()
		{
			var galeri = _db.Galeriler.ToList();
			return View(galeri);
		}
        // GET: Yonetici/Rezervasyons/Create
        public IActionResult Rezarvasyon()
        {
            return View();
        }

        // POST: Yonetici/Rezervasyons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezarvasyon([Bind("Id,Name,Email,TelefonNo,Sayi,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _db.Add(rezervasyon);
                await _db.SaveChangesAsync();
				_toast.AddSuccessToastMessage("Rezarvasyon işleminiz başarıyla oluşturulmuştur,teşekkür ederiz...");
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }

        public IActionResult Hakkında()
		{
			var hakkinda=_db.Hakkındalar.ToList();
			return View(hakkinda);
		}

        // GET: Yonetici/Blogs/Create-creat'i blog olarak değiştirdik
        public IActionResult Blog()
        {
            return View();
        }

        // POST: Yonetici/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Tarih=DateTime.Now;
                //yorum tarihini müşteri girmeyecek,sistemden çekiyoruz
                var files = HttpContext.Request.Form.Files;
                //IF DOSYA KONTROLÜ YAPTIM
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    //RESİM EKLEMEK İÇİN PATH METODU KULLANILIR
                    //RESMİ KAYDETMEK İSTEDİĞİM DOSYA YOLUNU BELİRTTİM
                    var uploads = Path.Combine(_whe.WebRootPath, @"Website\menu");
                    var extn = Path.GetExtension(files[0].FileName).ToLower();

                    if (blog.Image != null)
                    {
                        var ImagePath = Path.Combine(_whe.WebRootPath, blog.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extn), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    blog.Image = @"\Website\menu\" + fileName + extn;
                }
                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Yorumunuz iletildi.Onaylandığında size bildirilecektir.");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
		// GET: Yonetici/İletisim/Create
		public IActionResult Iletisim()
		{
			return View();
		}

		// POST: Yonetici/İletisim/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Iletisim(İletisim İletisim)
		{
			if (ModelState.IsValid)
			{
				İletisim.Tarih=DateTime.Now;
				_db.Add(İletisim);
				await _db.SaveChangesAsync();
				_toast.AddSuccessToastMessage("Mesajınız başarıyla iletildi");
				return RedirectToAction(nameof(Index));
			}
			return View(İletisim);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
