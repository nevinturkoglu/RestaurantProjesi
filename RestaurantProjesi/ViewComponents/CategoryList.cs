using Microsoft.AspNetCore.Mvc;
using RestaurantProjesi.Data;
using System.Linq;

namespace RestaurantProjesi.ViewComponents
{
	public class CategoryList : ViewComponent  //kalıtım aldırdık
	{
		//veritabanına bağlama işlemleri
		private readonly ApplicationDbContext _db;

		public CategoryList(ApplicationDbContext db) //yapıcı metod ctor tab tab
		{
			_db = db;
		}

		public IViewComponentResult Invoke()
		//Invoke bir IViewComponentResult döndüren zaman uyumlu yöntem.
		//invoke=çağırmak
		{
			var category=_db.Kategoriler.ToList();
			return View(category);
		}
	}
}
