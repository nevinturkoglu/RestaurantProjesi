﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantProjesi.Models
{
	public class İletisim
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Telefon { get; set; }
		[Required]
		public string Mesaj { get; set; }
		public DateTime Tarih { get; set; }

	}
}
