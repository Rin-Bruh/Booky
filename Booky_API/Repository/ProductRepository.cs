using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booky_API.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDBContext _db;
		public ProductRepository(ApplicationDBContext db)
		{
			_db = db;
		}
		public async Task Create(Product entity)
		{
			await _db.Products.AddAsync(entity);
			await Save();
		}

		public async Task<Product> Get(Expression<Func<Product, bool>> filter = null, bool tracked = true)
		{
			IQueryable<Product> query = _db.Products;
			if (!tracked)
			{
				query = query.AsNoTracking();
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null)
		{
			IQueryable<Product> query = _db.Products;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}

		public async Task Remove(Product entity)
		{
			_db.Products.Remove(entity);
			await Save();
		}

		public async Task Save()
		{
			await _db.SaveChangesAsync();
		}
	}
}
