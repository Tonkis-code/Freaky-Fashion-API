using FreakyFashion.api.Data;
using Microsoft.EntityFrameworkCore;
using FreakyFashion.api.Models;
using FreakyFashion.api.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/products", async (AppDbContext db, string? slug) =>
{
    var query = db.Products.AsQueryable();

    if (slug != null)
    {
        query = query.Where(p => p.UrlSlug == slug);
    }

    var products = await query.Select(p => new ProductResponseDto
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        Image = p.Image,
        UrlSlug = p.UrlSlug
    })
    .ToListAsync();

    return Results.Ok(products);
});

app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound();
    }
    var response = ToResponseDto(product);
    return Results.Ok(response);
});

app.MapPost("/api/products", async (AppDbContext db, CreateProductDto dto) =>
{
    var product = new Product
    {
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price,
        Image = dto.Image,
        UrlSlug = dto.Name.Trim().ToLowerInvariant().Replace(" ", "-")
    };

    db.Products.Add(product);
    await db.SaveChangesAsync();
    var response = ToResponseDto(product);
    return Results.Created($"/api/products/{product.Id}", response);

});

static ProductResponseDto ToResponseDto(Product product)
{
    return new ProductResponseDto
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Image = product.Image,
        UrlSlug = product.UrlSlug
    };
}

app.Run();