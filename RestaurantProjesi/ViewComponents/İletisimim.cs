using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using RestaurantProjesi.Data;
using System.Linq;

namespace RestaurantProjesi.ViewComponents
{
    public class İletisimim:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public İletisimim(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var iletisim = _db.İletisimimler.ToList();
            return View(iletisim);
        }
    }
}
