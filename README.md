
# Freaky Fashion API

A RESTful e-commerce backend built using **C#, ASP.NET Core Minimal API, Entity Framework Core, and SQL Server**.

This project is part of my **Backend Development 2** course at EC Utbildning, Sweden.

The purpose of this project is to strengthen my backend development skills through hands-on experience with RESTful API design, database management, dependency injection, and Entity Framework Core.

## Project Status

🚧 **Work in Progress**

All required product and category endpoints for the assignment have been implemented.

The API supports creating, retrieving, filtering, and deleting products and categories. Data is stored persistently in SQL Server LocalDB using Entity Framework Core.

The remaining work includes reviewing the implementation against the assignment requirements, verifying product-category relationships, and completing final testing and documentation.

## Tech Stack

- **C# / .NET 10** — Backend development
- **ASP.NET Core Minimal API** — RESTful API
- **Entity Framework Core** — Object-relational mapping (ORM)
- **SQL Server LocalDB** — Relational database
- **OpenAPI** — API documentation
- **Bruno** — Manual API testing
- **Git & GitHub** — Version control

## Features

### Currently Implemented

**Products**
- Creating products through a REST API
- Retrieving all products
- Retrieving individual products by ID
- Filtering products by URL slug
- Deleting products by ID
- Automatic URL slug generation
- Input validation for product creation
- Product request and response DTOs

**Categories**
- Creating categories through a REST API
- Retrieving all categories
- Retrieving individual categories by ID
- Filtering categories by URL slug
- Deleting categories by ID
- Automatic URL slug generation
- Input validation for category creation
- Category request and response DTOs
- Returning category data with a nested products collection

**General**
- Database persistence using Entity Framework Core
- SQL Server LocalDB integration
- Entity relationships between categories and products
- Asynchronous database operations
- Dependency injection
- HTTP status codes including `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, and `404 Not Found`

### Remaining Work

- Verify product-category relationships with associated product data
- Review all functionality against the assignment requirements
- Complete final API testing
- Finalize documentation

### Development Progress

- [x] ASP.NET Core Minimal API project setup
- [x] Product and Category database models
- [x] Entity Framework Core configuration
- [x] SQL Server LocalDB integration
- [x] Database migrations
- [x] Product creation DTO
- [x] Product response DTO
- [x] Category creation DTO
- [x] Category response DTO
- [x] GET endpoint for retrieving all products
- [x] GET endpoint for retrieving a product by ID
- [x] GET endpoint for filtering products by slug
- [x] POST endpoint for creating products
- [x] DELETE endpoint for deleting products
- [x] Automatic product URL slug generation
- [x] Product input validation
- [x] GET endpoint for retrieving all categories
- [x] GET endpoint for retrieving a category by ID
- [x] GET endpoint for filtering categories by slug
- [x] POST endpoint for creating categories
- [x] DELETE endpoint for deleting categories
- [x] Automatic category URL slug generation
- [x] Category input validation
- [x] Nested product DTO mapping in category responses
- [x] Manual endpoint testing using Bruno
- [ ] Verify category responses with associated products
- [ ] Final requirements review
- [ ] Final API testing and documentation

## API Endpoints

### Products

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| GET | `/api/products` | Retrieve all products | Implemented |
| GET | `/api/products/{id}` | Retrieve a product by ID | Implemented |
| GET | `/api/products?slug={slug}` | Retrieve products matching a URL slug | Implemented |
| POST | `/api/products` | Create a new product | Implemented |
| DELETE | `/api/products/{id}` | Delete a product | Implemented |

### Categories

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| GET | `/api/categories` | Retrieve categories and their products | Implemented |
| GET | `/api/categories/{id}` | Retrieve a category by ID | Implemented |
| GET | `/api/categories?slug={slug}` | Retrieve categories matching a URL slug | Implemented |
| POST | `/api/categories` | Create a new category | Implemented |
| DELETE | `/api/categories/{id}` | Delete a category | Implemented |

## API Examples

### Creating a Product

**POST `/api/products`**

Request body:

```json
{
  "name": "Black Cotton T-Shirt",
  "description": "A comfortable black cotton t-shirt.",
  "price": 199.99,
  "image": "/images/black-cotton-t-shirt.jpg"
}
```

The API automatically generates the product's URL slug and assigns its database ID.

Example response — **201 Created**:

```json
{
  "id": 1,
  "name": "Black Cotton T-Shirt",
  "description": "A comfortable black cotton t-shirt.",
  "price": 199.99,
  "image": "/images/black-cotton-t-shirt.jpg",
  "urlSlug": "black-cotton-t-shirt"
}
```

### Filtering Products by Slug

Products can be filtered using an optional query parameter:

```http
GET /api/products?slug=black-cotton-t-shirt
```

This returns an array containing matching products.

If no matching product exists, the endpoint returns:

```json
[]
```

With the HTTP status code `200 OK`.

If no slug is provided, the endpoint returns all products.

### Creating a Category

**POST `/api/categories`**

Request body:

```json
{
  "name": "T-Shirts",
  "image": "/images/tshirts.png"
}
```

The API automatically generates the category's URL slug and assigns its database ID.

Example response — **201 Created**:

```json
{
  "id": 1,
  "name": "T-Shirts",
  "image": "/images/tshirts.png",
  "urlSlug": "t-shirts",
  "products": []
}
```

Newly created categories have an empty products collection.

### Retrieving Categories

**GET `/api/categories`**

Returns a list of categories containing their basic information and a nested products collection.

**GET `/api/categories/{id}`**

Returns a specific category by its ID, including its associated products.

If the category does not exist, the endpoint returns `404 Not Found`.

### Filtering Categories by Slug

Categories can be filtered using an optional query parameter:

```http
GET /api/categories?slug=t-shirts
```

This returns an array containing matching categories.

If no category matches the supplied slug, the endpoint returns `200 OK` with an empty array.

### Deleting Resources

Products and categories can be deleted using their respective DELETE endpoints.

```http
DELETE /api/products/1
```

```http
DELETE /api/categories/1
```

A successful deletion returns `204 No Content`.

If the requested resource does not exist, the API returns `404 Not Found`.

## Getting Started

### Prerequisites

- .NET 10 SDK
- Visual Studio with ASP.NET and web development tools, or another compatible IDE
- SQL Server LocalDB
- Entity Framework Core tooling
- Bruno or another API testing tool (optional)

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/Tonkis-code/Freaky-Fashion-API.git
```

**2. Navigate to the project**

```bash
cd Freaky-Fashion-API
```

**3. Configure the database**

The application uses SQL Server LocalDB with a connection string defined in `appsettings.json`.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=FreakyFashionDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**4. Apply database migrations**

Open the solution in Visual Studio.

Navigate to **Tools → NuGet Package Manager → Package Manager Console**.

Select `FreakyFashion.api` as the default project and run:

```powershell
Update-Database
```

This creates the database and applies the existing migrations.

**5. Run the application**

Start the application through Visual Studio or run:

```bash
dotnet run --project FreakyFashion.api
```

The API will be accessible at the local address displayed in the console.

**6. Test the API**

Use Bruno or another HTTP client to send requests to the running application.

For example:

```http
GET /api/products
```

```http
GET /api/products/1
```

```http
GET /api/products?slug=black-cotton-t-shirt
```

```http
GET /api/categories
```

```http
GET /api/categories?slug=t-shirts
```

## Project Structure

```text
FreakyFashion/
├── FreakyFashion.api/
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── DTOs/
│   │   ├── CreateProductDto.cs
│   │   ├── ProductResponseDto.cs
│   │   ├── CreateCategoryDto.cs
│   │   └── CategoryResponseDto.cs
│   ├── Models/
│   │   ├── Product.cs
│   │   └── Category.cs
│   ├── Migrations/
│   ├── Properties/
│   ├── Program.cs
│   ├── appsettings.json
│   └── FreakyFashion.api.csproj
├── FreakyFashion.sln
└── README.md
```

*The structure may evolve as the project grows.*

## Architecture and Implementation

### Minimal API

The backend uses ASP.NET Core Minimal API to define HTTP endpoints directly in `Program.cs`.

This approach provides a lightweight way to build RESTful APIs while learning routing, dependency injection, and HTTP request handling.

### Entity Framework Core

Entity Framework Core handles communication between the application and SQL Server.

The application uses an `AppDbContext` containing `DbSet` properties for products and categories.

Database migrations are used to create and update the database schema.

### Data Transfer Objects

DTOs separate the API's request and response structures from the database entities.

The project uses four DTOs:

- `CreateProductDto` — Defines the data required when creating a product.
- `ProductResponseDto` — Defines the product data returned to API clients.
- `CreateCategoryDto` — Defines the data required when creating a category.
- `CategoryResponseDto` — Defines category data returned to API clients, including a products collection.

This separation allows the database models and API contracts to evolve independently.

Helper methods convert database entities into response DTOs to avoid exposing database models directly.

### Product and Category Relationships

The `Category` model contains a collection of related products.

Entity Framework Core manages the relationship between categories and products.

Category GET endpoints return product information using nested `ProductResponseDto` objects.

The category GET-by-ID endpoint uses `Include()` to load related products before converting the category into its response DTO.

The relationship mapping has been implemented, but additional testing with associated products remains.

### URL Slug Generation

Product and category URL slugs are generated automatically from their names.

For example:

```text
Black Cotton T-Shirt
```

Becomes:

```text
black-cotton-t-shirt
```

The current implementation trims surrounding whitespace, converts text to lowercase, and replaces spaces with hyphens.

More advanced slug normalization and uniqueness handling may be added later.

### Database Queries

The endpoints use asynchronous Entity Framework Core queries.

LINQ methods such as `Where()`, `Select()`, `FirstOrDefaultAsync()`, and `ToListAsync()` are used to filter, retrieve, and transform data.

The category GET-by-ID endpoint also uses `Include()` to load related products.

### Input Validation

POST endpoints validate incoming data before creating database entities.

Product creation rejects:

- Empty or whitespace-only names
- Empty or whitespace-only descriptions
- Empty or whitespace-only image paths
- Prices less than or equal to zero

Category creation rejects:

- Empty or whitespace-only names
- Empty or whitespace-only image paths

Invalid requests return `400 Bad Request`.

## API Testing

Manual API testing is performed using **Bruno**.

The following scenarios have been tested during development:

- Creating products and categories
- Retrieving products and categories
- Retrieving resources by ID
- Filtering products and categories by URL slug
- Receiving an empty array when a slug has no matches
- Deleting resources successfully
- Receiving `404 Not Found` for nonexistent resources
- Receiving `204 No Content` after successful category deletion

Additional testing is planned to verify populated category-product relationships and complete the final requirements review.

## Learning Objectives

Through this project, I'm gaining practical experience with:

- Building RESTful APIs using ASP.NET Core Minimal API
- Understanding HTTP methods and status codes
- Dependency injection and application configuration
- Working with SQL Server through Entity Framework Core
- Creating and managing database migrations
- Understanding relationships between database entities
- Implementing DTOs to separate API contracts from database models
- Writing LINQ queries for filtering and transforming data
- Understanding route parameters and query parameters
- Asynchronous programming using `async` and `await`
- Writing maintainable and structured backend code
- Testing APIs using an HTTP client
- Managing source code using Git and GitHub

## Potential Future Improvements

Once the assignment requirements are completed, potential improvements include:

- Pagination and additional filtering
- Updating existing products and categories
- More advanced input validation
- Improved URL slug generation and uniqueness handling
- Automated API testing
- Improved API documentation
- A frontend application consuming the API

## About This Project

Freaky Fashion is a school assignment developed as part of the **Frontend Developer program at EC Utbildning**.

The assignment defines the functional requirements, while the backend implementation is my own work.

This repository documents my development process and progress as I continue building my skills in C#, ASP.NET Core, SQL Server, and backend development.

---

**Developed by [Tonkis-code](https://github.com/Tonkis-code)**
