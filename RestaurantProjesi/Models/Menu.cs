using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantProjesi.Models
{
	public class Menu
	{
		[Key] //Id benzersiz olacak

		public int Id { get; set; }
		[Required] //alan boş geçilmeyecek
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public bool OzelMenu { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set;} //her menünün bir kategorisi olacak

		[ForeignKey("CategoryId")]

		public Kategori Category { get; set; }//menü classını kategori classı ile ilişkilendirdik

	}
}
