using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.ProjectModel;
using WebApi.Controllers;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Services;
using WebApi.Extensions;
using WebApi.Utility;
using Xunit;

namespace UnitTests;

public class UnitTests
{
    [Fact]
    public async void ProductController_GetProducts_Ok()
    {
        var moq = new Mock<IProductService>();
        moq.Setup(s => s.GetProductsAsync()).ReturnsAsync(GetMockData());
        var controller = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller.GetProducts();

        Assert.IsType<OkObjectResult>(result.Result);
    }


    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    [InlineData(100)]
    public async void ProductController_GetProducts_ProductCount(int count)
    {
        var moq = new Mock<IProductService>();
        moq.Setup(s => s.GetProductsAsync()).ReturnsAsync(GetMockData(count));
        var controller = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller.GetProducts();

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(count, ((result.Result as OkObjectResult)!.Value as IEnumerable<ProductDto>)!.Count());
    }


    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public async void ProductController_GetProductById_Ok(int id)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockData();
        moq.Setup(s => s.GetProductAsync(id)).ReturnsAsync(mockData.FirstOrDefault(m => m.Id == id));
        var controller = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller.GetProduct(id);

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(id, ((result.Result as OkObjectResult)!.Value as ProductDto)!.Id);
    }


    [Theory]
    [InlineData(999)]
    [InlineData(888)]
    [InlineData(777)]
    [InlineData(666)]
    public async void ProductController_GetProductById_NotFound(int id)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockData();
        moq.Setup(s => s.GetProductAsync(id)).ReturnsAsync(mockData.FirstOrDefault(m => m.Id == id));
        var controller = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller.GetProduct(id);

        Assert.IsType<NotFoundResult>(result.Result);
    }


    [Theory]
    [InlineData(-1, -1)]
    [InlineData(0, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    public async void ProductController_GetProductPage_BadRequest(int page, int pageSize)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockDataPaginated(page: page, pageSize: pageSize);
        moq.Setup(s => s.GetProductsAsync(page, pageSize)).ReturnsAsync(mockData);
        var controller = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller.GetPaginatedProducts(page, pageSize);

        Assert.IsType<BadRequestResult>(result.Result);
    }


    [Theory]
    [InlineData(1, 1, 1, 1, 1)]
    [InlineData(50, 2, 25, 50, 2)]
    [InlineData(100, 5, 10, 100, 10)]
    [InlineData(200, 2, 50, 200, 4)]
    public async void ProductController_GetProductPage_Ok(int count, int page, int pageSize, int totalItems, int totalPages)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockDataPaginated(count, page, pageSize);
        moq.Setup(s => s.GetProductsAsync(page, pageSize)).ReturnsAsync(mockData);
        var controller = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller.GetPaginatedProducts(page, pageSize);
        var resultObject = ((result.Result as OkObjectResult)!.Value as ProductPaginationDto)!;

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(page, resultObject.PaginationMetadata.CurrentPage);
        Assert.Equal(pageSize, resultObject.PaginationMetadata.PageSize);
        Assert.Equal(totalItems, resultObject.PaginationMetadata.TotalRecords);
        Assert.Equal(totalPages, resultObject.PaginationMetadata.TotalPages);
        Assert.Equal(resultObject.Products.Count(), resultObject.PaginationMetadata.PageSize);
    }

    [Theory]
    [InlineData(999, "lorem")]
    [InlineData(888, "ipsum")]
    public async void ProductDescriptionController_UpdateDescription_ProductNotFound(int productId, string description)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockData();
        moq.Setup(s => s.ProductExists(productId)).ReturnsAsync(mockData.Any(m => m.Id == productId));
        var controller = new ProductDescriptionController(moq.Object, Mock.Of<ILogger<ProductDescriptionController>>());

        var result = await controller.UpdateProductDescription(productId, new ProductDescriptionDto { Id = productId, Description = description });

        Assert.IsType<NotFoundResult>(result);
    }

    [Theory]
    [InlineData(999, "lorem")]
    [InlineData(888, "ipsum")]
    public async void ProductDescriptionController_UpdateDescription_BadRequest(int productId, string description)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockData();
        moq.Setup(s => s.ProductExists(productId)).ReturnsAsync(mockData.Any(m => m.Id == productId));
        var controller = new ProductDescriptionController(moq.Object, Mock.Of<ILogger<ProductDescriptionController>>());

        var result = await controller.UpdateProductDescription(productId, new ProductDescriptionDto { Id = productId + 1, Description = description });

        Assert.IsType<BadRequestResult>(result);
    }


    [Theory]
    [InlineData(1, "lorem")]
    [InlineData(2, "ipsum")]
    public async void ProductDescriptionController_UpdateDescription_Ok(int productId, string description)
    {
        var moq = new Mock<IProductService>();
        var mockData = GetMockData();
        moq.Setup(s => s.ProductExists(productId)).ReturnsAsync(mockData.Any(m => m.Id == productId));
        moq.Setup(s => s.GetProductAsync(productId)).ReturnsAsync(mockData.FirstOrDefault(m => m.Id == productId));
        moq.Setup(s => s.UpdateProductDescriptionAsync(productId, description)).Callback(() => mockData.FirstOrDefault(m => m.Id == productId)!.Description = description).ReturnsAsync(true);
        var controller1 = new ProductDescriptionController(moq.Object, Mock.Of<ILogger<ProductDescriptionController>>());
        var controller2 = new ProductController(moq.Object, Mock.Of<ILogger<ProductController>>());

        var result = await controller1.UpdateProductDescription(productId, new ProductDescriptionDto { Id = productId, Description = description });
        var product = await controller2.GetProduct(productId);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal(description, ((product.Result as OkObjectResult)!.Value as ProductDto)!.Description);
    }


    public IEnumerable<ProductDto> GetMockData(int count = 20)
    {
        return Enumerable.Range(1, count)
            .Select(i => new ProductDto
            {
                Id = i,
                Name = Randomize.GetRandomProductName(),
                Description = Randomize.GetRandomProductDescription(),
                Price = Randomize.GetRandomProductPrice(),
                ImgUri = $"/pictures/{i}.jpg",
            }).ToList();
    }


    public ProductPaginationDto GetMockDataPaginated(int count = 20, int page = 1, int pageSize = 10)
    {
        var result = new ProductPaginationDto { };
        var products = GetMockData(count).ToList();

        result.Products = products.Skip((page - 1) * pageSize).Take(pageSize);
        result.PaginationMetadata.CurrentPage = page;
        result.PaginationMetadata.PageSize = pageSize;
        result.PaginationMetadata.TotalRecords = products.Count();
        result.PaginationMetadata.TotalPages = (int)Math.Ceiling(result.PaginationMetadata.TotalRecords.SafeDiv(pageSize));
        result.PaginationMetadata.PreviousPage = page <= 1 ? null : page - 1;
        result.PaginationMetadata.NextPage = result.PaginationMetadata.TotalPages <= page ? null : page + 1;

        return result;
    }
}