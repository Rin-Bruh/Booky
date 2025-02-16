using AutoMapper;
using Booky_API.Models;
using Booky_API.Models.Dto;

namespace Booky_API
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Product, ProductDTO>();
			CreateMap<ProductDTO, Product>();

			CreateMap<Product, ProductCreateDTO>().ReverseMap();
			CreateMap<Product, ProductUpdateDTO>().ReverseMap();

			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<Category, CategoryCreateDTO>().ReverseMap();
			CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
			CreateMap<ApplicationIdentityUser, UserDTO>().ReverseMap();
		}
	}
}
