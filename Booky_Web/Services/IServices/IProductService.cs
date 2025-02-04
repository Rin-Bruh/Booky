using Booky_Web.Models.Dto;

namespace Booky_Web.Services.IServices
{
	public interface IProductService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetAsync<T>(int id, string token);
		Task<T> CreateAsync<T>(ProductCreateDTO dto, string token);
		Task<T> UpdateAsync<T>(ProductUpdateDTO dto, string token);
		Task<T> DeleteAsync<T>(int id, string token);
	}
}
