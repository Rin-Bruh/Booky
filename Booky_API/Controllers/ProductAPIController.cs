﻿using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
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

		public ProductAPIController(ApplicationDBContext db)
		{
			_db = db;
			//_logger = logger;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<ProductDTO>> GetProducts()
		{
			//_logger.Log("Getting all product", "");
			return Ok(_db.Products.ToList());
		}

		[HttpGet("{id:int}",Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ProductDTO> GetProduct(int id)
		{
			if (id == 0)
			{
				//_logger.Log("Get Product Error with Id" + id, "error");
				return BadRequest();
			}
			var product = _db.Products.FirstOrDefault(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		//[Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ProductDTO>> CreateVilla([FromBody] ProductDTO productDTO)
		{
			//try
			//{
			//if (!ModelState.IsValid)
			//{
			//    return BadRequest(ModelState);
			//}
			if (_db.Products.FirstOrDefault(u => u.Title.ToLower() == productDTO.Title.ToLower()) != null)
			{
				ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
				return BadRequest(ModelState);
			}
			if (productDTO == null)
			{
				//ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
				return BadRequest(ModelState);
			}

			if (productDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
				
			//Villa villa = _mapper.Map<Villa>(createDTO);
			Product model = new()
			{
				Title = productDTO.Title,
				Description = productDTO.Description,
				ISBN = productDTO.ISBN,
				Author = productDTO.Author,
				ListPrice = productDTO.ListPrice,
				Price = productDTO.Price,
				Price50 = productDTO.Price50,
				Price100 = productDTO.Price100,
				ImageUrl = productDTO.ImageUrl,
				CategoryId = productDTO.CategoryId
			};
			_db.Products.Add(model);
			_db.SaveChanges();
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
			return CreatedAtRoute("GetProduct", new { id = productDTO.Id}, productDTO);
		}

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		//[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id:int}", Name = "DeleteProduct")]
		public IActionResult DeleteProduct(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}
			var product = _db.Products.FirstOrDefault(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			_db.Products.Remove(product);
			_db.SaveChanges();
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateProduct(int id, [FromBody] ProductDTO productDTO)
		{
			if (productDTO == null || id != productDTO.Id)
			{
				return BadRequest();
			}
			//var product = _db.Products.FirstOrDefault(u => u.Id == id);
			//product.Title = productDTO.Title;
			//product.ISBN = productDTO.ISBN;
			//product.Description = productDTO.Description;
			//product.Author = productDTO.Author;
			Product model = new()
			{
				Id = productDTO.Id,
				Title = productDTO.Title,
				Description = productDTO.Description,
				ISBN = productDTO.ISBN,
				Author = productDTO.Author,
				ListPrice = productDTO.ListPrice,
				Price = productDTO.Price,
				Price50 = productDTO.Price50,
				Price100 = productDTO.Price100,
				ImageUrl = productDTO.ImageUrl,
				CategoryId = productDTO.CategoryId
			};
			_db.Products.Update(model);
			_db.SaveChanges();
			return NoContent();
		}

		[HttpPatch("{id:int}", Name = "UpdatePartialProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdatePartialProduct(int id, JsonPatchDocument<ProductDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var product = _db.Products.AsNoTracking().FirstOrDefault(u => u.Id == id);
			ProductDTO productDTO = new()
			{
				Id = product.Id,
				Title = product.Title,
				Description = product.Description,
				ISBN = product.ISBN,
				Author = product.Author,
				ListPrice = product.ListPrice,
				Price = product.Price,
				Price50 = product.Price50,
				Price100 = product.Price100,
				ImageUrl = product.ImageUrl,
				CategoryId = product.CategoryId
			};
			if (product == null)
			{
				return BadRequest();
			}
			patchDTO.ApplyTo(productDTO, ModelState);
			Product model = new()
			{
				Id = productDTO.Id,
				Title = productDTO.Title,
				Description = productDTO.Description,
				ISBN = productDTO.ISBN,
				Author = productDTO.Author,
				ListPrice = productDTO.ListPrice,
				Price = productDTO.Price,
				Price50 = productDTO.Price50,
				Price100 = productDTO.Price100,
				ImageUrl = productDTO.ImageUrl,
				CategoryId = productDTO.CategoryId
			};
			_db.Products.Update(model);
			_db.SaveChanges();
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return NoContent();
		}
	}
}
