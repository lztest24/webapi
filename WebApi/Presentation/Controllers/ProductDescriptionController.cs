using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using AutoMapper;
using WebApi.Models;
using SQLitePCL;
using Asp.Versioning;
using WebApi.Extensions;
using MediatR;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductDescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;


        public ProductDescriptionController(IMediator mediator, ILogger<ProductDescriptionController> logger)
        {
            _mediator = mediator;
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
            if (!ModelState.IsValid)
            {
                _logger.LogError("{loginfo} - invalid params", this.GetLogInfo(await Request.GetRawBodyAsync()));
                return BadRequest();
            }

            if (id != descriptionDto.Id)
            {
                _logger.LogError("{loginfo} - invalid request", this.GetLogInfo(await Request.GetRawBodyAsync()));
                return BadRequest();
            }

            if (!await _mediator.Send(new ProductExistsQuery(id), token))
            {
                _logger.LogError("{loginfo} - product not found", this.GetLogInfo(await Request.GetRawBodyAsync()));
                return NotFound();
            }

            if (await _mediator.Send(new UpdateProductDescriptionCommand(descriptionDto.Id, descriptionDto.Description!), token))
                return NoContent();
            else
                return StatusCode(500);
        }

    }
}
