using System.ComponentModel.DataAnnotations;

namespace RestaurantProjesi.Models
{
    public class Kategori
    {
        [Key] // ID Attribute ile benzersiz yaptık

        public int Id { get; set; }
        [Required] //Name boş geçilmemesi için attribute tanımladık.

        public string Name { get; set; }
    }
}
