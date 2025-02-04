using Booky_Web.Models;

namespace Booky_Web.Services.IServices
{
	public interface IBaseService
	{
		APIResponse responseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest apiRequest);
	}
}
