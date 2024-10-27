using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Entities;
using WebApi.Repositories;
using AutoMapper;
using WebApi.Models;
using Asp.Versioning;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;


        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        [ApiVersion(1)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return Ok(
                await _service.GetProductsAsync()
            );

        }


        /// <summary>
        /// Get a single page of products.
        /// </summary>
        /// <param name="page">Page number to retrieve.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>A list of products with pagination metadata.</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid page number or page size.</response>
        [HttpGet]
        [ApiVersion(2)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductPaginationDto>> GetPaginatedProducts(int page, int? pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest();

            if (pageSize == null)
                pageSize = 10;

            return Ok(
                await _service.GetProductsAsync(page, pageSize.Value)
            );

        }


        /// <summary>
        /// Get a single product.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>A single product.</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Product with given ID not found.</response>        
        [HttpGet("{id}")]
        [ApiVersion(1)]
        [ApiVersion(2)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _service.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // [HttpPatch("{id}")]
        // public async Task<IActionResult> UpdateProductDescription(int id, JsonPatchDocument patchDocument)
        // {
        //     var product = await _repository.GetProductAsync(id);

        //     if (product == null)
        //     {
        //         return NotFound();
        //     }

        //     var prodDescPatch = new ProductDescriptionDto { Description = product.Description };

        //     patchDocument.ApplyTo(prodDescPatch);

        //     product.Description = prodDescPatch.Description;

        //     await _repository.SaveChangesAsync();

        //     return NoContent();
        // }

    }
}
