using AutoMapper;
using Booky_Utility;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Services;
using Booky_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Booky_Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		public async Task<IActionResult> IndexProduct()
		{
			List<ProductDTO> list = new();

			var response = await _productService.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		//[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateProduct()
		{
			return View();
		}
		//[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProduct(ProductCreateDTO model)
		{
			if (ModelState.IsValid)
			{

				var response = await _productService.CreateAsync<APIResponse>(model);
				if (response != null && response.IsSuccess)
				{
					//TempData["success"] = "Product created successfully";
					return RedirectToAction(nameof(IndexProduct));
				}
			}
			//TempData["error"] = "Error encountered.";
			return View(model);
		}
	}
}
