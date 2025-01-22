using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace Booky_API.Controllers
{
	//[Route("api/[controller]")]
	[Route("api/ProductAPI")]
	[ApiController]
	public class ProductAPIController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<ProductDTO> GetProducts()
		{
			return ProductStore.productList;
		}
		[HttpGet("{id:int}")]
		public ProductDTO GetProduct(int id)
		{
			return ProductStore.productList.FirstOrDefault(u=>u.Id==id);
		}
	}
}
