# Freaky Fashion API

A RESTful e-commerce backend built using **C#, ASP.NET Core Minimal API, Entity Framework Core, and SQL Server**.

This project is part of my **Backend Development 2** course at EC Utbildning, Sweden.

The purpose of this project is to strengthen my backend development skills through hands-on experience with RESTful API design, database management, dependency injection, and Entity Framework Core.

## Project Status

🚧 **Work in Progress**

Freaky Fashion is currently under active development.

The API supports creating products, retrieving all products, retrieving individual products by ID, and filtering products by URL slug. Product data is stored persistently in SQL Server LocalDB using Entity Framework Core.

Category endpoints, product deletion, and additional validation are still under development.

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

- Creating products through a REST API
- Retrieving all products
- Retrieving individual products by ID
- Filtering products by URL slug
- Automatic URL slug generation when creating products
- Database persistence using Entity Framework Core
- Data Transfer Objects (DTOs) for product requests and responses
- Asynchronous database operations
- HTTP status codes including `200 OK`, `201 Created`, and `404 Not Found`

### Planned Features

- Deleting products
- Input validation for product creation
- Creating, retrieving, and deleting categories
- Retrieving categories and their associated products
- Category lookup by ID or URL slug
- Automatic URL slug generation for categories
- Category request and response DTOs
- Additional API testing and documentation

### Development Progress

- [x] ASP.NET Core Minimal API project setup
- [x] Product and Category database models
- [x] Entity Framework Core configuration
- [x] SQL Server LocalDB integration
- [x] Initial database migration
- [x] Product creation DTO
- [x] Product response DTO
- [x] GET endpoint for retrieving all products
- [x] GET endpoint for retrieving a product by ID
- [x] GET endpoint for filtering products by slug
- [x] POST endpoint for creating products
- [x] Automatic product URL slug generation
- [x] Manual testing of product endpoints using Bruno
- [ ] Product input validation
- [ ] Product deletion
- [ ] Category DTOs
- [ ] Category endpoints
- [ ] Category and product relationship handling in API responses
- [ ] Final API testing and documentation

## API Endpoints

### Products

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| GET | `/api/products` | Retrieve all products | Implemented |
| GET | `/api/products/{id}` | Retrieve a product by ID | Implemented |
| GET | `/api/products?slug={slug}` | Retrieve products matching a URL slug | Implemented |
| POST | `/api/products` | Create a new product | Implemented |
| DELETE | `/api/products/{id}` | Delete a product | Planned |

### Categories

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| GET | `/api/categories` | Retrieve categories and their products | Planned |
| GET | `/api/categories/{id}` | Retrieve a category by ID | Planned |
| GET | `/api/categories?slug={slug}` | Retrieve categories matching a URL slug | Planned |
| POST | `/api/categories` | Create a new category | Planned |
| DELETE | `/api/categories/{id}` | Delete a category | Planned |

### Example Product Request

**POST `/api/products`**

```json
{
  "name": "Black Cotton T-Shirt",
  "description": "A comfortable black cotton t-shirt.",
  "price": 199.99,
  "image": "/images/black-cotton-t-shirt.jpg"
}
```

The API automatically generates the product's URL slug and assigns its database ID.

### Example Product Response

**201 Created**

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

## Project Structure

```text
FreakyFashion/
├── FreakyFashion.api/
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── DTOs/
│   │   ├── CreateProductDto.cs
│   │   └── ProductResponseDto.cs
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

Currently implemented DTOs include:

- `CreateProductDto` — Defines the data required when creating a product.
- `ProductResponseDto` — Defines the product data returned to API clients.

This separation allows the database models and API contracts to evolve independently.

### URL Slug Generation

Product URL slugs are generated automatically from product names.

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

Product endpoints use asynchronous Entity Framework Core queries.

LINQ methods such as `Where()`, `Select()`, `FirstOrDefaultAsync()`, and `ToListAsync()` are used to filter, retrieve, and transform data.

Product responses are mapped to DTOs rather than returning database entities directly.

## API Testing

Manual API testing is performed using **Bruno**.

The following scenarios have been tested successfully:

- Creating a product and receiving `201 Created`
- Retrieving products from the database
- Retrieving a product by ID
- Receiving `404 Not Found` for a nonexistent product ID
- Filtering products by URL slug
- Retrieving all products when no slug is provided
- Receiving an empty array when a slug has no matches

Further testing will be performed as additional endpoints are implemented.

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
