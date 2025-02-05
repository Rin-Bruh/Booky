using Booky_Utility;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Services.IServices;

namespace Booky_Web.Services
{
	public class ProductService : BaseService, IProductService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string productUrl;

		public ProductService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			productUrl = configuration.GetValue<string>("ServiceUrls:BookyAPI");

		}
		public Task<T> CreateAsync<T>(ProductCreateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = productUrl + "/api/v1/productAPI"
				//Token = token
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = productUrl + "/api/v1/productAPI/"+id
				//Token = token
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = productUrl + "/api/productAPI"
				//Token = token
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = productUrl + "/api/v1/productAPI/"+id
				//Token = token
			});
		}

		public Task<T> UpdateAsync<T>(ProductUpdateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = productUrl + "/api/v1/productAPI/"+dto.Id,
				//Token = token
			});
		}
	}
}
