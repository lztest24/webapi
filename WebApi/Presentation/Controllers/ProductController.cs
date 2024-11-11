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
using WebApi.Extensions;
using Asp.Versioning;
using Microsoft.Extensions.Logging;
using WebApi.Utility;
using MediatR;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        [ApiVersion(1)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductsDto>> GetProducts(CancellationToken token = default)
        {
            var result = await _mediator.Send(new GetProductsQuery { }, token);
            return Ok(result);
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
        public async Task<ActionResult<ProductPaginationDto>> GetPaginatedProducts(int page, int? pageSize, CancellationToken token = default)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("{loginfo} - invalid params", this.GetLogInfo(ModelState));
                return BadRequest();
            }

            if (page <= 0 || pageSize <= 0)
            {
                _logger.LogError("{loginfo} - invalid request", this.GetLogInfo(ModelState));
                return BadRequest();
            }

            if (pageSize == null)
                pageSize = 10;

            var result = await _mediator.Send(new GetProductPageQuery(page, pageSize.Value), token);


            return Ok(result);
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
        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken token = default)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("{loginfo} - invalid params", this.GetLogInfo(ModelState));
                return BadRequest();
            }

            var product = await _mediator.Send(new GetProductQuery(id), token);

            if (product == null)
            {
                _logger.LogError("{loginfo} - product not found", this.GetLogInfo(ModelState));
                return NotFound();
            }

            return Ok(product);
        }


    }
}
