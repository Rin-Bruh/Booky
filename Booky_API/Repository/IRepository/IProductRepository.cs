using Booky_API.Models;
using System.Linq.Expressions;

namespace Booky_API.Repository.IRepository
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null);
		Task<Product> Get(Expression<Func<Product, bool>> filter = null, bool tracked = true);
		Task Create(Product entity);
		Task Remove(Product entity);
		Task Save();
	}
}
