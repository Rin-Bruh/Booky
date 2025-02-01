using Booky_API.Models;
using System.Linq.Expressions;

namespace Booky_API.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<Product> UpdateAsync(Product entity);
	}
}
