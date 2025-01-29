using Booky_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Booky_API.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
			: base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
	}
}
