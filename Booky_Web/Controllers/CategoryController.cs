using AutoMapper;
using Booky_Web.Models;
using Booky_Web.Models.Dto;
using Booky_Web.Services;
using Booky_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Booky_Web.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		public async Task<IActionResult> IndexCategory()
		{
			List<CategoryDTO> list = new();

			var response = await _categoryService.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}
		//[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateCategory()
		{
			return View();
		}
		//[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateCategory(CategoryCreateDTO model)
		{
			if (ModelState.IsValid)
			{

				var response = await _categoryService.CreateAsync<APIResponse>(model);
				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Category created successfully";
					return RedirectToAction(nameof(IndexCategory));
				}
			}
			TempData["error"] = "Error encountered.";
			return View(model);
		}
		public async Task<IActionResult> UpdateCategory(int categoryId)
		{
			var response = await _categoryService.GetAsync<APIResponse>(categoryId);
			if (response != null && response.IsSuccess)
			{
				CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(response.Result));
				return View(_mapper.Map<CategoryUpdateDTO>(model));
			}
			return View();
		}
		//[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateCategory(CategoryUpdateDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _categoryService.UpdateAsync<APIResponse>(model);
				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Category updated successfully";
					return RedirectToAction(nameof(IndexCategory));
				}
			}
			TempData["error"] = "Error encountered.";
			return View(model);
		}

		public async Task<IActionResult> DeleteCategory(int categoryId)
		{
			var response = await _categoryService.GetAsync<APIResponse>(categoryId);
			if (response != null && response.IsSuccess)
			{
				CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(response.Result));
				return View(model);
			}
			return View();
		}
		//[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteCategory(CategoryDTO model)
		{
			var response = await _categoryService.DeleteAsync<APIResponse>(model.Id);
			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Category deleted successfully";
				return RedirectToAction(nameof(IndexCategory));
			}
			TempData["error"] = "Error encountered.";
			return View(model);
		}
	}
}
