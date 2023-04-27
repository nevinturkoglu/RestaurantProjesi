using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantProjesi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantProjesi.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		//models klasöründe oluşturduğumuz modelleri dbcontextte tanımlıyoruz.
		//veritabanına tablo eklenecekse dbset bu alanda oluşturulur.
		public DbSet<Kategori>Kategoriler { get; set; }
		public DbSet<Menu> Menuler { get; set; }
		public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
		public DbSet<Galeri> Galeriler { get; set; }
		public DbSet<Hakkında> Hakkındalar { get; set; }
		public DbSet<Blog> Bloglar { get; set; }
		public DbSet<İletisim> İletisimler { get; set; }
		public DbSet<İletisimim> İletisimimler{ get; set; }
		public DbSet<AppUser> AppUserlar { get; set; }

	}
}
