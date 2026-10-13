# Freaky Fashion API

A RESTful e-commerce backend built using **C#, ASP.NET Core Minimal API, Entity Framework Core, and SQL Server**.

This project is part of my **Backend Development 2** course at EC Utbildning, Sweden.

The purpose of this project is to strengthen my backend development skills through hands-on experience with RESTful API design, database management, dependency injection, and Entity Framework Core.

## Project Status

🚧 **Work in Progress**

Freaky Fashion is currently under active development. Features and documentation will be updated as the project progresses.

## Tech Stack

- **C# / .NET 10** — Backend development
- **ASP.NET Core Minimal API** — RESTful API
- **Entity Framework Core** — Object-relational mapping (ORM)
- **SQL Server LocalDB** — Relational database
- **OpenAPI** — API documentation
- **Git & GitHub** — Version control

## Features

The completed API is intended to support:

- Creating, retrieving, and deleting products
- Retrieving individual products by ID or URL slug
- Creating, retrieving, and deleting categories
- Retrieving categories and their associated products
- Database persistence using Entity Framework Core
- Data Transfer Objects (DTOs) for requests and responses
- Input validation and appropriate HTTP status codes
- Automatic URL slug generation

### Development Progress

- [x] ASP.NET Core Minimal API project setup
- [x] Product and Category models
- [x] Entity Framework Core configuration
- [x] SQL Server LocalDB integration
- [x] Initial database migration
- [x] GET endpoint for retrieving products
- [x] Initial POST endpoint for creating products
- [ ] Product DTOs and validation
- [ ] Automatic URL slug generation
- [ ] Product lookup by ID and slug
- [ ] Product deletion
- [ ] Category endpoints
- [ ] API testing and final documentation

## API Endpoints

The following endpoints are planned for the completed API.

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Retrieve all products |
| GET | `/api/products/{id}` | Retrieve a product by ID |
| GET | `/api/products?slug={slug}` | Retrieve products matching a URL slug |
| POST | `/api/products` | Create a new product |
| DELETE | `/api/products/{id}` | Delete a product |
| GET | `/api/categories` | Retrieve categories and their products |
| GET | `/api/categories/{id}` | Retrieve a category by ID |
| GET | `/api/categories?slug={slug}` | Retrieve categories matching a URL slug |
| POST | `/api/categories` | Create a new category |
| DELETE | `/api/categories/{id}` | Delete a category |

*Some endpoints are still under development.*

## Getting Started

### Prerequisites

- .NET 10 SDK
- Visual Studio with ASP.NET and web development tools, or another compatible IDE
- SQL Server LocalDB
- Entity Framework Core tooling

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

## Project Structure

```text
FreakyFashion/
├── FreakyFashion.api/
│   ├── Data/
│   │   └── AppDbContext.cs
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

## Learning Objectives

Through this project, I'm gaining practical experience with:

- Building RESTful APIs using ASP.NET Core Minimal API
- Dependency injection and application configuration
- Working with SQL Server through Entity Framework Core
- Creating and managing database migrations
- Understanding relationships between database entities
- Implementing DTOs to separate API contracts from database models
- Asynchronous programming using `async` and `await`
- Writing maintainable and structured backend code

## Potential Future Improvements

Once the assignment requirements are completed, potential improvements include:

- Pagination and filtering
- Updating existing products and categories
- Automated API testing
- Improved API documentation
- A frontend application consuming the API

## About This Project

Freaky Fashion is a school assignment developed as part of the **Frontend Developer program at EC Utbildning**.

The assignment defines the functional requirements, while the backend implementation is my own work.

This repository documents my development process and progress as I continue building my skills in C#, ASP.NET Core, and backend development.

---

**Developed by [Tonkis-code](https://github.com/Tonkis-code)**
