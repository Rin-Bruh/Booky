using System.ComponentModel.DataAnnotations;

namespace Booky_API.Models.Dto
{
	public class CategoryCreateDTO
	{
		[Required]
		public string Name { get; set; }
		public int DisplayOrder { get; set; }
	}
}
