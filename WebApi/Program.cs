using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContext") ?? throw new InvalidOperationException("Connection string 'ProductContext' not found.")));
builder.Services.AddScoped<WebApi.Interfaces.IProductRepository, WebApi.Repositories.ProductRepository>();
builder.Services.AddScoped<WebApi.Interfaces.IProductService, WebApi.Services.ProductService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddControllers()
    .AddXmlDataContractSerializerFormatters();

// builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/WebApi.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSerilog();


builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddApiVersioning(setupAction =>
    {
        setupAction.ReportApiVersions = true;
        setupAction.AssumeDefaultVersionWhenUnspecified = true;
        setupAction.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    })
    .AddMvc()
    .AddApiExplorer(setupAction =>
    {
        setupAction.SubstituteApiVersionInUrl = true;
        setupAction.GroupNameFormat = "'v'VVV";
    });

var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        setupAction.SwaggerDoc(
            apiVersionDescription.GroupName,
            new()
            {
                Title = "Web Product API",
                Version = $"{apiVersionDescription.ApiVersion}",
                Description = "This API is used to retrieve products or update their descriptions."
            }
        );
    }
});

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.Use(next => context => {
    context.Request.EnableBuffering();
    return next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        var apiDescriptions = app.DescribeApiVersions();
        foreach (var apiDescription in apiDescriptions)
        {
            setupAction.SwaggerEndpoint(
                $"/swagger/{apiDescription.GroupName}/swagger.json",
                apiDescription.GroupName
            );
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
