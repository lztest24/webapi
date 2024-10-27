using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using AutoMapper;
using WebApi.Models;
using SQLitePCL;
using Asp.Versioning;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductDescriptionController : ControllerBase
    {
        private readonly IProductService _service;


        public ProductDescriptionController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Update the description of a product.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <param name="descriptionDto">The product ID and new description.</param>
        /// <response code="204">Description updated.</response>
        /// <response code="400">Product IDs do not match.</response>   
        /// <response code="404">Product with given ID not found.</response>   
        [HttpPut("{id}")]
        [ApiVersion(1)]
        [ApiVersion(2)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductDescription(int id, ProductDescriptionDto descriptionDto)
        {
            if (id != descriptionDto.Id)
            {
                return BadRequest();
            }

            if (!await _service.ProductExists(id))
            {
                return NotFound();
            }

            await _service.UpdateProductDescriptionAsync(descriptionDto.Id, descriptionDto.Description);

            return NoContent();
        }

    }
}
