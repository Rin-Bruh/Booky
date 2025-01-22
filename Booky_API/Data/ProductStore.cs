using Booky_API.Models.Dto;

namespace Booky_API.Data
{
	public class ProductStore
	{
		public static List<ProductDTO> productList = new List<ProductDTO>
			{
				new ProductDTO { Id = 1, Title = "Cotton Candy"},
				new ProductDTO { Id = 2, Title = "Rock in the Ocean" }
			};
	}
}
