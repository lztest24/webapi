using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using AutoMapper;
using WebApi.Models;
using SQLitePCL;
using Asp.Versioning;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductDescriptionController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger _logger;


        public ProductDescriptionController(IProductService service, ILogger<ProductDescriptionController> logger)
        {
            _service = service;
            _logger = logger;
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
        public async Task<IActionResult> UpdateProductDescription(int id, ProductDescriptionDto descriptionDto, CancellationToken token = default)
        {
            try
            {
                if (id != descriptionDto.Id)
                {
                    _logger.LogError("{loginfo}({@params}) - invalid request", this.GetLogInfo(), new { id, descriptionDto });
                    return BadRequest();
                }

                if (!await _service.ProductExists(id, token))
                {
                    _logger.LogError("{loginfo}({@params}) - product not found", this.GetLogInfo(), new { id, descriptionDto });
                    return NotFound();
                }

                if (await _service.UpdateProductDescriptionAsync(descriptionDto.Id, descriptionDto.Description, token))
                    return NoContent();
                else
                    return StatusCode(500);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("{loginfo}({@params}) - task canceled", this.GetLogInfo(), new { id, descriptionDto });
                return StatusCode(499);
            }
            catch (Exception ex)
            {
                _logger.LogError("{loginfo}({@params}) - exception thrown: {exception}", this.GetLogInfo(), new { id, descriptionDto }, ex);
                return StatusCode(500);
            }
        }

    }
}
