using Booky_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booky_API.Data
{
	public class ApplicationDBContext : IdentityDbContext<ApplicationIdentityUser>
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
			: base(options)
		{
		}
		public DbSet<ApplicationIdentityUser> ApplicationIdentityUsers { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
				new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
				new Category { Id = 3, Name = "History", DisplayOrder = 3 }
				);

			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Title = "Fortune of Time",
					Author = "Billy Spark",
					Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
					ISBN = "SWD9999001",
					ListPrice = 450000,
					Price = 400000,
					Price50 = 390000,
					Price100 = 370000,
					ImageUrl = "\\images\\product\\Fortune of Time.jpg",
					CategoryId = 1,
					CreatedDate = DateTime.Now
				},
			  new Product
			  {
				  Id = 2,
				  Title = "Dark Skies",
				  Author = "Nancy Hoover",
				  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
				  ISBN = "CAW777777701",
				  ListPrice = 75000,
				  Price = 70000,
				  Price50 = 65000,
				  Price100 = 60000,
				  ImageUrl = "\\images\\product\\Dark Skies.jpg",
				  CategoryId = 1,
				  CreatedDate = DateTime.Now
			  },
			  new Product
			  {
				  Id = 3,
				  Title = "Vanish in the Sunset",
				  Author = "Julian Button",
				  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
				  ISBN = "RITO5555501",
				  ListPrice = 250000,
				  Price = 200000,
				  Price50 = 190000,
				  Price100 = 180000,
				  ImageUrl = "\\images\\product\\Vanish in the Sunset.jpg",
				  CategoryId = 1,
				  CreatedDate = DateTime.Now
			  },
			  new Product
			  {
				  Id = 4,
				  Title = "Cotton Candy",
				  Author = "Abby Muscles",
				  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
				  ISBN = "WS3333333301",
				  ListPrice = 350000,
				  Price = 300000,
				  Price50 = 290000,
				  Price100 = 280000,
				  ImageUrl = "\\images\\product\\Cotton Candy.jpg",
				  CategoryId = 2,
				  CreatedDate = DateTime.Now
			  },
			  new Product
			  {
				  Id = 5,
				  Title = "Rock in the Ocean",
				  Author = "Ron Parker",
				  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
				  ISBN = "SOTJ1111111101",
				  ListPrice = 150000,
				  Price = 140000,
				  Price50 = 130000,
				  Price100 = 120000,
				  ImageUrl = "\\images\\product\\Rock in the Ocean.jpg",
				  CategoryId = 2,
				  CreatedDate = DateTime.Now
			  },
			  new Product
			  {
				  Id = 6,
				  Title = "Leaves and Wonders",
				  Author = "Laura Phantom",
				  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
				  ISBN = "FOT000000001",
				  ListPrice = 120000,
				  Price = 115000,
				  Price50 = 110000,
				  Price100 = 100000,
				  ImageUrl = "\\images\\product\\Leaves and Wonders.jpg",
				  CategoryId = 3,
				  CreatedDate = DateTime.Now
			  });
		}
	}
}
