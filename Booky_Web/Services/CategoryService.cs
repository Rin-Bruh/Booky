using Booky_Utility;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Services.IServices;

namespace Booky_Web.Services
{
	public class CategoryService : BaseService, ICategoryService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string categoryUrl;

		public CategoryService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			categoryUrl = configuration.GetValue<string>("ServiceUrls:BookyAPI");

		}
		public Task<T> CreateAsync<T>(CategoryCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = categoryUrl + "/api/categoryAPI",
				Token = token
			});
		}

		public Task<T> DeleteAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = categoryUrl + "/api/categoryAPI/" + id,
				Token = token
			});
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = categoryUrl + "/api/categoryAPI",
				Token = token
			});
		}

		public Task<T> GetAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = categoryUrl + "/api/categoryAPI/" + id,
				Token = token
			});
		}

		public Task<T> UpdateAsync<T>(CategoryUpdateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = categoryUrl + "/api/categoryAPI/" + dto.Id,
				Token = token
			});
		}
	}
}
