using Booky_API.Models;
using System.Linq.Expressions;

namespace Booky_API.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		Task<Category> UpdateAsync(Category entity);
	}
}
