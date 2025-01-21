using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Booky_API.Models
{
	public class Products
	{
		
		public int Id { get; set; }
		public string Title { get; set; }
	}
}
