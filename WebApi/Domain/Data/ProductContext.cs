using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
    {
    }

    public DbSet<WebApi.Entities.Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seedProducts = Enumerable.Range(1, 50)
            .Select(i => new Product
        {
            Id = i,
            Name = WebApi.Utility.Randomize.GetRandomProductName(),
            Description = WebApi.Utility.Randomize.GetRandomProductDescription(),
            Price = WebApi.Utility.Randomize.GetRandomProductPrice(),
            ImgUri = $"/pictures/{i}.jpg",
        });

        modelBuilder.Entity<Product>().HasData(seedProducts);

        base.OnModelCreating(modelBuilder);
    }
}
