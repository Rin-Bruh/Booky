using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booky_API.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDBContext _db;
		public CategoryRepository(ApplicationDBContext db): base(db)
		{
			_db = db;
		}
		

		public async Task<Category> UpdateAsync(Category entity)
		{
			_db.Categories.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
