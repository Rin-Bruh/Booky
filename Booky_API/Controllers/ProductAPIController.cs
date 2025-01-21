using Booky_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booky_API.Controllers
{
	[Route("api/ProductAPI")]
	[ApiController]
	public class ProductAPIController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Products> GetProducts()
		{
			return new List<Products>
			{
				new Products { Id = 1, Title = "Cotton Candy"},
				new Products { Id = 2, Title = "Rock in the Ocean" }
			};
		}
	}
}
