using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantProjesi.Models
{
	public class AppUser : IdentityUser //class user tablosuna ekleme yapabilmesi için ıdentityuser miras almalı
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[NotMapped]
		public string Role { get; set; } //ORM ARAÇLARINDA KARŞILIĞI OLMAYAN KOLON BELİRTTİM

	}
}
