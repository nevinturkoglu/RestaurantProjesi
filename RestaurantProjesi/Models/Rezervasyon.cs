using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantProjesi.Models
{
	public class Rezervasyon
	{
		//sqlde tablo oluşturabilmek için özellik-property tanımlıyoruz
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string TelefonNo { get; set; }
		[Required]
		public int Sayi { get; set; }
		[Required]
		public string Saat { get; set; }
		public DateTime Tarih { get; set; }

	}
}
