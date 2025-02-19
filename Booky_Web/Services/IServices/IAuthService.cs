﻿using Booky_Web.Models.Dto;

namespace Booky_Web.Services.IServices
{
	public interface IAuthService
	{
		Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
		Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
	}
}
