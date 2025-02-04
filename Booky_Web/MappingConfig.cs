using AutoMapper;
using Booky_Web.Models.Dto;

namespace Booky_Web
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<ProductDTO, ProductCreateDTO>().ReverseMap();
			CreateMap<ProductDTO, ProductUpdateDTO>().ReverseMap();

			CreateMap<CategoryDTO, CategoryCreateDTO>().ReverseMap();
			CreateMap<CategoryDTO, CategoryUpdateDTO>().ReverseMap();
		}
	}
}
