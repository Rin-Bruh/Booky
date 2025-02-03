using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booky_Web.Models.Dto
{
	public class CategoryDTO
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int DisplayOrder { get; set; }
	}
}
