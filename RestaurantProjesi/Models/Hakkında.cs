using System.ComponentModel.DataAnnotations;

namespace RestaurantProjesi.Models
{
	public class Hakkında
	{
		[Key]
		public int Id { get; set; }
        public string Title { get; set; }
    }
}
