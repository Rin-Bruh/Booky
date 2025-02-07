using System.ComponentModel.DataAnnotations;

namespace Booky_API.Models.Dto
{
	public class ProductDTO
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public string Title { get; set; }
		public string Description { get; set; }
		[Required]
		public string ISBN { get; set; }
		[Required]
		public string Author { get; set; }
		[Required]
		public double ListPrice { get; set; }
		public double Price { get; set; }
		public double Price50 { get; set; }
		public double Price100 { get; set; }
		public string ImageUrl { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public CategoryDTO Category { get; set; }
	}
}
