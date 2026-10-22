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


// Products Endpoints
// GET /api/products where we get all products or filter by slug
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

// GET /api/products/{id} where we get a product by id
// where we use ToResponseDto to convert the product to a ProductResponseDto to not expose the database model directly to the client
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

// DELETE /api/products/{id} where we delete a product by id
// where we return 404 if the product is not found
// and we return 204 if the product is deleted successfully
app.MapDelete("/api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound();
    }
    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// POST /api/products where we create a new product
// here we validate the input and return 400 if the input is invalid
app.MapPost("/api/products", async (AppDbContext db, CreateProductDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0 || string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.Image))
    {
        return Results.BadRequest();
    }
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

// Categories Endpoints
// GET /api/categories where we get all categories
// here we have to use ProductResponseDto to return the products in the category and not expose the database model directly to the client
app.MapGet("/api/categories", async (AppDbContext db, string? slug) =>
{
    var query = db.Categories.AsQueryable();

    if (slug != null)
    {
        query = query.Where(c => c.UrlSlug == slug);
    }

    var categories = await query.Select(c => new CategoryResponseDto
    {
        Id = c.Id,
        Name = c.Name,
        Image = c.Image,
        UrlSlug = c.UrlSlug,

        Products = c.Products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Image = p.Image,
            UrlSlug = p.UrlSlug
        }).ToList()
    })
      .ToListAsync();

    return Results.Ok(categories);
});

// POST /api/categories where we create a new category
// where we use ToCategoryResponseDto to convert the category to a CategoryResponseDto to not expose the database model directly to the client
app.MapPost("/api/categories", async (AppDbContext db, CreateCategoryDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Image))
    {
        return Results.BadRequest();
    }

    var category = new Category
    {
        Name = dto.Name,
        Image = dto.Image,
        UrlSlug = dto.Name.Trim().ToLowerInvariant().Replace(" ", "-")
    };

    db.Categories.Add(category);
    await db.SaveChangesAsync();
    var response = ToCategoryResponseDto(category);
    return Results.Created($"/api/categories/{category.Id}", response);
});

// GET /api/categories/{id} where we get a category by id
// Include loads the products belonging to the category
// We use a response DTO to avoid exposing the database model directly to the client
app.MapGet("/api/categories/{id}", async (int id, AppDbContext db) =>
{
    var category = await db.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    var response = ToCategoryResponseDto(category);
    return Results.Ok(response);
});

// DELETE /api/categories/{id} where we delete a category by id
// if the category is null we return 404 but if there is a category we remove it and return 204
app.MapDelete("/api/categories/{id}", async (int id, AppDbContext db) =>
{
    var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    db.Categories.Remove(category);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// Static Response DTOs
// CategoryResponseDto method to not expose the database model directly to the client
static CategoryResponseDto ToCategoryResponseDto(Category category)
{
    return new CategoryResponseDto
    {
        Id = category.Id,
        Name = category.Name,
        Image = category.Image,
        UrlSlug = category.UrlSlug,
        Products = category.Products.Select(p => ToResponseDto(p)).ToList()
    };
}

// ProductResponseDto method to not expose the database model directly to the client
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