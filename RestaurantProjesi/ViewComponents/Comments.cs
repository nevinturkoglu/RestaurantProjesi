using Microsoft.AspNetCore.Mvc;
using RestaurantProjesi.Data;
using System.Linq;

namespace RestaurantProjesi.ViewComponents
{
	public class Comments : ViewComponent
	{
		private readonly ApplicationDbContext _db;
		public Comments(ApplicationDbContext db)
		{
			_db = db;

		}
		public IViewComponentResult Invoke()
		{
			/*var comment=_db.Blogs.ToList();*/// yönetici onayı olmadan tüm yorumları blog sayfasında gösterir!
			var comment = _db.Bloglar.Where(i => i.Onay).ToList(); //Sadece onaylı yorumları listeledim
			return View(comment);
		}
	}
}
