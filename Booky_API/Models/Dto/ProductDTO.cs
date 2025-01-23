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
	}
}
