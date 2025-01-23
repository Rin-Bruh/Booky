using Booky_API.Models.Dto;

namespace Booky_API.Data
{
	public class ProductStore
	{
		public static List<ProductDTO> productList = new List<ProductDTO>
			{
				new ProductDTO { Id = 1, Title = "Cotton Candy", Description = "kkkk", ISBN = "WS3333333301", Author = "Abby Muscles"},
				new ProductDTO { Id = 2, Title = "Rock in the Ocean", Description = "llll", ISBN = "SOTJ1111111101", Author = "Ron Parker"}
			};
	}
}
