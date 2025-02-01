using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booky_API.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDBContext _db;
		public ProductRepository(ApplicationDBContext db): base(db)
		{
			_db = db;
		}
		

		public async Task<Product> UpdateAsync(Product entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.Products.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
