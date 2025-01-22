using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Booky_API.Models
{
	public class Product
	{
		
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
