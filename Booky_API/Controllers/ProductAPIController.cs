using AutoMapper;
using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Models.Dto;
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
		private readonly ApplicationDBContext _db;
		private readonly IMapper _mapper;

		public ProductAPIController(ApplicationDBContext db, IMapper mapper)
		{
			//_logger = logger;
			_db = db;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
		{
			//_logger.Log("Getting all product", "");
			IEnumerable<Product> productList = await _db.Products.ToListAsync();
			return Ok(_mapper.Map<List<ProductDTO>>(productList));
		}

		[HttpGet("{id:int}",Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductDTO>> GetProduct(int id)
		{
			if (id == 0)
			{
				//_logger.Log("Get Product Error with Id" + id, "error");
				return BadRequest();
			}
			var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<ProductDTO>(product));
		}

		[HttpPost]
		//[Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ProductDTO>> CreateVilla([FromBody] ProductCreateDTO createDTO)
		{
			//try
			//{
			//if (!ModelState.IsValid)
			//{
			//    return BadRequest(ModelState);
			//}
			if (await _db.Products.FirstOrDefaultAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
			{
				ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
				return BadRequest(ModelState);
			}
			if (createDTO == null)
			{
				//ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
				return BadRequest(createDTO);
			}

			//if (productDTO.Id > 0)
			//{
			//	return StatusCode(StatusCodes.Status500InternalServerError);
			//}

			Product model = _mapper.Map<Product>(createDTO);
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
			await _db.Products.AddAsync(model);
			await _db.SaveChangesAsync();
			//await _dbVilla.CreateAsync(villa);
			//_response.Result = _mapper.Map<VillaDTO>(villa);
			//_response.StatusCode = HttpStatusCode.Created;
			//return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
			//}
			//catch (Exception ex)
			//{
			//	_response.IsSuccess = false;
			//	_response.ErrorMessages
			//		 = new List<string>() { ex.ToString() };
			//}
			return CreatedAtRoute("GetProduct", new { id = model.Id}, model);
		}

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		//[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id:int}", Name = "DeleteProduct")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}
			var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			_db.Products.Remove(product);
			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDTO)
		{
			if (updateDTO == null || id != updateDTO.Id)
			{
				return BadRequest();
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
			_db.Products.Update(model);
			await _db.SaveChangesAsync();
			return NoContent();
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
			var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
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
			_db.Products.Update(model);
			await _db.SaveChangesAsync();
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return NoContent();
		}
	}
}
