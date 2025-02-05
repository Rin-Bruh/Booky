﻿using Booky_Web.Models.Dto;

namespace Booky_Web.Services.IServices
{
	public interface IProductService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetAsync<T>(int id);
		Task<T> CreateAsync<T>(ProductCreateDTO dto);
		Task<T> UpdateAsync<T>(ProductUpdateDTO dto);
		Task<T> DeleteAsync<T>(int id);
	}
}
