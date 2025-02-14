using Booky_Utility;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Services.IServices;

namespace Booky_Web.Services
{
	public class AuthService : BaseService, IAuthService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string authUrl;

		public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			authUrl = configuration.GetValue<string>("ServiceUrls:BookyAPI");

		}

		public Task<T> LoginAsync<T>(LoginRequestDTO obj)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = obj,
				Url = authUrl + "/api/UsersAuth/login"
				//Token = token
			});
		}

		public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = obj,
				Url = authUrl + "/api/UsersAuth/register"
				//Token = token
			});
		}
	}
}
