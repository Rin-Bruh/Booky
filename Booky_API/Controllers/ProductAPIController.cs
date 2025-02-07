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
	[Route("api/ProductAPI")]
	[ApiController]
	public class ProductAPIController : ControllerBase
	{
		//private readonly ILogging _logger;
		protected APIResponse _response;
		private readonly IProductRepository _dbProduct;
		private readonly ICategoryRepository _dbCategory;
		private readonly IMapper _mapper;

		public ProductAPIController(IProductRepository dbProduct, IMapper mapper, ICategoryRepository dbCategory)
		{
			//_logger = logger;
			_dbProduct = dbProduct;
			_mapper = mapper;
			this._response = new();
			_dbCategory = dbCategory;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetProducts()
		{
			try
			{
				//_logger.Log("Getting all product", "");
				IEnumerable<Product> productList = await _dbProduct.GetAllAsync(includeProperties:"Category");
				_response.Result = _mapper.Map<List<ProductDTO>>(productList);
				_response.StatusCode = HttpStatusCode.OK;
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

		[HttpGet("{id:int}",Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetProduct(int id)
		{
			try { 
				if (id == 0)
				{
					//_logger.Log("Get Product Error with Id" + id, "error");
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var product = await _dbProduct.GetAsync(u => u.Id == id);
				if (product == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				_response.Result = _mapper.Map<ProductDTO>(product);
				_response.StatusCode = HttpStatusCode.OK;
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
		//[Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDTO createDTO)
		{
			try
			{
				//if (!ModelState.IsValid)
				//{
				//    return BadRequest(ModelState);
				//}
				if (await _dbProduct.GetAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Product already Exists!");
					return BadRequest(ModelState);
				}
				if (await _dbCategory.GetAsync(u => u.Id == createDTO.CategoryId) == null)
				{
					ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
					return BadRequest(ModelState);
				}
				if (createDTO == null)
				{
					//ModelState.AddModelError("ErrorMessages", "Product already Exists!");
					return BadRequest(createDTO);
				}

				//if (productDTO.Id > 0)
				//{
				//	return StatusCode(StatusCodes.Status500InternalServerError);
				//}

				Product product = _mapper.Map<Product>(createDTO);
				//Product model = new()
				//{
				//	Title = createDTO.Title,
				//	Description = createDTO.Description,
				//	ISBN = createDTO.ISBN,
				//	Author = createDTO.Author,
				//	ListPrice = createDTO.ListPrice,
				//	Price = createDTO.Price,
				//	Price50 = createDTO.Price50,
				//	Price100 = createDTO.Price100,
				//	ImageUrl = createDTO.ImageUrl,
				//	CategoryId = createDTO.CategoryId
				//};
				await _dbProduct.CreateAsync(product);
				_response.Result = _mapper.Map<ProductDTO>(product);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetProduct", new { id = product.Id}, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		//[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id:int}", Name = "DeleteProduct")]
		public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
		{
			try { 
				if(id == 0)
				{
					return BadRequest();
				}
				var product = await _dbProduct.GetAsync(u => u.Id == id);
				if (product == null)
				{
					return NotFound();
				}
				await _dbProduct.RemoveAsync(product);
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

		[HttpPut("{id:int}", Name = "UpdateProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDTO)
		{
			try { 
				if (updateDTO == null || id != updateDTO.Id)
				{
					return BadRequest();
				}
				if (await _dbCategory.GetAsync(u => u.Id == updateDTO.CategoryId) == null)
				{
					ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
					return BadRequest(ModelState);
				}
				Product model = _mapper.Map<Product>(updateDTO);
				//Product model = new()
				//{
				//	Id = updateDTO.Id,
				//	Title = updateDTO.Title,
				//	Description = updateDTO.Description,
				//	ISBN = updateDTO.ISBN,
				//	Author = updateDTO.Author,
				//	ListPrice = updateDTO.ListPrice,
				//	Price = updateDTO.Price,
				//	Price50 = updateDTO.Price50,
				//	Price100 = updateDTO.Price100,
				//	ImageUrl = updateDTO.ImageUrl,
				//	CategoryId = updateDTO.CategoryId
				//};
				await _dbProduct.UpdateAsync(model);
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

		[HttpPatch("{id:int}", Name = "UpdatePartialProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdatePartialProduct(int id, JsonPatchDocument<ProductUpdateDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var product = await _dbProduct.GetAsync(u => u.Id == id, tracked: false);
			ProductUpdateDTO productDTO = _mapper.Map<ProductUpdateDTO>(product);
			//ProductUpdateDTO productDTO = new()
			//{
			//	Id = product.Id,
			//	Title = product.Title,
			//	Description = product.Description,
			//	ISBN = product.ISBN,
			//	Author = product.Author,
			//	ListPrice = product.ListPrice,
			//	Price = product.Price,
			//	Price50 = product.Price50,
			//	Price100 = product.Price100,
			//	ImageUrl = product.ImageUrl,
			//	CategoryId = product.CategoryId
			//};
			if (product == null)
			{
				return BadRequest();
			}
			patchDTO.ApplyTo(productDTO, ModelState);
			Product model = _mapper.Map<Product>(productDTO);
			//Product model = new()
			//{
			//	Id = productDTO.Id,
			//	Title = productDTO.Title,
			//	Description = productDTO.Description,
			//	ISBN = productDTO.ISBN,
			//	Author = productDTO.Author,
			//	ListPrice = productDTO.ListPrice,
			//	Price = productDTO.Price,
			//	Price50 = productDTO.Price50,
			//	Price100 = productDTO.Price100,
			//	ImageUrl = productDTO.ImageUrl,
			//	CategoryId = productDTO.CategoryId
			//};
			await _dbProduct.UpdateAsync(model);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return NoContent();
		}
	}
}
