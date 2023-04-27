using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantProjesi.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProjesi.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
	[Authorize]
	public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users=_context.AppUserlar.ToList();
            var role=_context.Roles.ToList();
            var userRol=_context.UserRoles.ToList();
            foreach (var item in users)
            {
                var roleId=userRol.FirstOrDefault(i=>i.UserId==item.Id).RoleId; //ilişkili farklı tablolardaki user role getirdim
                item.Role = role.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return View(users);
        }

        // GET: Yonetici/Kategoris/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AppUserlar
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Yonetici/Kategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.AppUserlar.FindAsync(id);
            _context.AppUserlar.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategoriler.Any(e => e.Id == id);
        }
    }
}

    
