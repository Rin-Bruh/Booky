using AutoMapper;
using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Models.Dto;
using Booky_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml;

namespace Booky_API.Controllers
{
	//[Route("api/[controller]")]
	[Route("api/CategoryAPI")]
	[ApiController]
	public class CategoryAPIController : ControllerBase
	{
		//private readonly ILogging _logger;
		protected APIResponse _response;
		private readonly ICategoryRepository _dbCategory;
		private readonly IMapper _mapper;

		public CategoryAPIController(ICategoryRepository dbCategory, IMapper mapper)
		{
			//_logger = logger;
			_dbCategory = dbCategory;
			_mapper = mapper;
			this._response = new();
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetCategories()
		{
			try
			{
				//_logger.Log("Getting all product", "");
				IEnumerable<Category> categoryList = await _dbCategory.GetAllAsync();
				_response.Result = _mapper.Map<List<CategoryDTO>>(categoryList);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
            {
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpGet("{id:int}",Name = "GetCategory")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetCategory(int id)
		{
			try { 
				if (id == 0)
				{
					//_logger.Log("Get Product Error with Id" + id, "error");
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					_response.ErrorMessages.Add("Id is incorrect");
					return BadRequest(_response);
				}
				var category = await _dbCategory.GetAsync(u => u.Id == id);
				if (category == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					_response.ErrorMessages.Add("Id not found");
					return NotFound(_response);
				}
				_response.Result = _mapper.Map<CategoryDTO>(category);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryCreateDTO createDTO)
		{
			try
			{
				if (await _dbCategory.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Category already Exists!");
					return BadRequest(ModelState);
				}
				if (createDTO == null)
				{
					//ModelState.AddModelError("ErrorMessages", "Category already Exists!");
					return BadRequest(createDTO);
				}
				Category category = _mapper.Map<Category>(createDTO);
				await _dbCategory.CreateAsync(category);
				_response.Result = _mapper.Map<CategoryDTO>(category);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetCategory", new { id = category.Id}, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}
		[Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		//[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id:int}", Name = "DeleteCategory")]
		public async Task<ActionResult<APIResponse>> DeleteCategory(int id)
		{
			try { 
				if(id == 0)
				{
					return BadRequest();
				}
				var category = await _dbCategory.GetAsync(u => u.Id == id);
				if (category == null)
				{
					return NotFound();
				}
				await _dbCategory.RemoveAsync(category);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}
		[Authorize(Roles = "admin")]
		[HttpPut("{id:int}", Name = "UpdateCategory")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] CategoryUpdateDTO updateDTO)
		{
			try { 
				if (updateDTO == null || id != updateDTO.Id)
				{
					return BadRequest();
				}
				Category model = _mapper.Map<Category>(updateDTO);
				await _dbCategory.UpdateAsync(model);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}
	}
}
