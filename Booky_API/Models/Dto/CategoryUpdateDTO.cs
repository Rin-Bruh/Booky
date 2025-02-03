using System.ComponentModel.DataAnnotations;

namespace Booky_API.Models.Dto
{
	public class CategoryUpdateDTO
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int DisplayOrder { get; set; }
	}
}
