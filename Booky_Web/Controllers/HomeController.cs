using AutoMapper;
using Booky_Utility;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Booky_Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		public HomeController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			List<ProductDTO> list = new();

			var response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

	}
}
