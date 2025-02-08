using AutoMapper;
using Booky_Utility;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Models.VM;
using Booky_Web.Services;
using Booky_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Booky_Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
		{
			_productService = productService;
			_mapper = mapper;
			_categoryService = categoryService;
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
			ProductCreateVM productVM = new();
			var response = await _categoryService.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
					(Convert.ToString(response.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}); ;
			}
			return View(productVM);
		}
		//[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProduct(ProductCreateVM model)
		{
			if (ModelState.IsValid)
			{
				var response = await _productService.CreateAsync<APIResponse>(model.Product);
				if (response != null && response.IsSuccess)
				{
					//TempData["success"] = "Product created successfully";
					return RedirectToAction(nameof(IndexProduct));
				}
			}
			var resp = await _categoryService.GetAllAsync<APIResponse>();
			if (resp != null && resp.IsSuccess)
			{
				model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>
					(Convert.ToString(resp.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}); ;
			}
			//TempData["error"] = "Error encountered.";
			return View(model);
		}
	}
}
