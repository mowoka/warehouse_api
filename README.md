# DataWarehouse API

A RESTful Web API built with **ASP.NET Core (.NET 10)** following a clean layered architecture and Object-Oriented Programming (OOP) principles.

---

## Table of Contents

- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [OOP Concepts Applied](#oop-concepts-applied)
- [Getting Started](#getting-started)
- [Database Migration](#database-migration)
- [Adding a New Seeder](#adding-a-new-seeder)
- [Adding a New Endpoint Group](#adding-a-new-endpoint-group)
- [API Documentation](#api-documentation)

---

## Project Structure

```
src/
├── DataWarehouse.Domain/           # Core entities (no dependencies)
│   └── Product.cs
│
├── DataWarehouse.Application/      # Business logic & contracts
│   ├── Interfaces/
│   │   └── IProductRepository.cs
│   └── Services/
│       └── ProductService.cs
│
├── DataWarehouse.Infrastructure/   # Database & external concerns
│   ├── DataWarehouseContext.cs
│   ├── Migrations/
│   ├── Repositories/
│   │   └── ProductRepository.cs
│   └── Seeders/
│       ├── ISeeder.cs
│       ├── DatabaseSeeder.cs
│       └── ProductSeeder.cs
│
└── DataWarehouse.Api/              # Entry point, HTTP layer
    ├── Endpoints/
    │   └── ProductEndpoints.cs
    ├── Program.cs
    └── appsettings.json
```

---

## Architecture

This project is separated into **4 layers**. Each layer has a single responsibility and can only depend on layers below it:

```
┌─────────────────────────────┐
│         API Layer           │  Receives HTTP requests, returns HTTP responses
├─────────────────────────────┤
│     Application Layer       │  Business rules, service classes, repository contracts
├─────────────────────────────┤
│    Infrastructure Layer     │  Database implementation (EF Core + PostgreSQL)
├─────────────────────────────┤
│       Domain Layer          │  Plain entity classes, no external dependencies
└─────────────────────────────┘
```

**Dependency direction:** API → Application → Domain, and Infrastructure → Domain.
Infrastructure implements contracts defined in Application, but Application never references Infrastructure directly.

---

## OOP Concepts Applied

### 1. Encapsulation
`ProductService` hides the internal details of how data is accessed from the API layer. The API only calls service methods without knowing anything about the database.

```csharp
// API only knows this
var products = await service.GetAllProductsAsync();
```

### 2. Abstraction
`IProductRepository` defines *what* operations are available without exposing *how* they work. This is the contract between Application and Infrastructure.

```csharp
// Application/Interfaces/IProductRepository.cs
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
}
```

### 3. Inheritance
`DataWarehouseContext` inherits from EF Core's `DbContext`, gaining all database management capabilities.

```csharp
// Infrastructure/DataWarehouseContext.cs
public class DataWarehouseContext : DbContext { ... }
```

### 4. Polymorphism
`ProductRepository` implements `IProductRepository`. In the future, a different implementation (e.g., `CachedProductRepository`) can replace it without changing `ProductService` or the API — they only depend on the interface.

```csharp
// Infrastructure/Repositories/ProductRepository.cs
public class ProductRepository : IProductRepository { ... }
```

The seeder system also uses polymorphism — `DatabaseSeeder` accepts `IEnumerable<ISeeder>`, so it works with any seeder without knowing the concrete type.

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Install EF Core CLI (once, globally)

```bash
dotnet tool install --global dotnet-ef
```

### Configure the connection string

Edit `src/DataWarehouse.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=db_warehouse;Username=postgres;Password=yourpassword"
  }
}
```

### Run the application

```bash
cd src/DataWarehouse.Api
dotnet run
```

Open the Scalar API UI in your browser at the URL shown in the terminal output, e.g.:
```
http://localhost:5000/scalar/v1
```

---

## Database Migration

All commands must be run from the **root of the repository**.

### Create a new migration

Run this after making any changes to an entity class (e.g., adding a new property to `Product`):

```bash
dotnet ef migrations add <MigrationName> \
  --project src/DataWarehouse.Infrastructure \
  --startup-project src/DataWarehouse.Api
```

Example:
```bash
dotnet ef migrations add InitialCreate \
  --project src/DataWarehouse.Infrastructure \
  --startup-project src/DataWarehouse.Api
```

### Apply migrations to the database

```bash
dotnet ef database update \
  --project src/DataWarehouse.Infrastructure \
  --startup-project src/DataWarehouse.Api
```

### List all migrations

```bash
dotnet ef migrations list \
  --project src/DataWarehouse.Infrastructure \
  --startup-project src/DataWarehouse.Api
```

### Rollback to a specific migration

```bash
dotnet ef database update <MigrationName> \
  --project src/DataWarehouse.Infrastructure \
  --startup-project src/DataWarehouse.Api
```

---

## Adding a New Seeder

When you need to add seed data for a new entity (e.g., `Order`), follow these steps:

### Step 1 — Create the seeder class

Create a new file in `src/DataWarehouse.Infrastructure/Seeders/`:

```csharp
// src/DataWarehouse.Infrastructure/Seeders/OrderSeeder.cs
using DataWarehouse.Domain;

namespace DataWarehouse.Infrastructure.Seeders;

public class OrderSeeder : ISeeder
{
    public async Task SeedAsync(DataWarehouseContext context)
    {
        if (context.Orders.Any()) return; // skip if data already exists

        var orders = new List<Order>
        {
            new() { /* properties */ },
        };

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();
    }
}
```

### Step 2 — Register the seeder in `Program.cs`

Add a single line inside the seeder registration block:

```csharp
// Register seeders
builder.Services.AddScoped<ISeeder, ProductSeeder>();
builder.Services.AddScoped<ISeeder, OrderSeeder>(); // add this line
builder.Services.AddScoped<DatabaseSeeder>();
```

`DatabaseSeeder` automatically picks up all registered `ISeeder` implementations and runs them in order.

---

## Adding a New Endpoint Group

When you need to add endpoints for a new entity, follow these steps:

### Step 1 — Create an endpoint file

Create a new file in `src/DataWarehouse.Api/Endpoints/`:

```csharp
// src/DataWarehouse.Api/Endpoints/OrderEndpoints.cs
namespace DataWarehouse.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/orders", async (OrderService service) =>
        {
            var orders = await service.GetAllOrdersAsync();
            return Results.Ok(orders);
        })
        .WithName("GetAllOrders")
        .WithTags("Orders");
    }
}
```

### Step 2 — Register in `Program.cs`

```csharp
app.MapProductEndpoints();
app.MapOrderEndpoints(); // add this line
```

---

## API Documentation

Scalar API documentation is available in the **Development** environment at:

```
http://localhost:<port>/scalar/v1
```

### Available Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/products` | Get all products |
| GET | `/products/{id}` | Get a product by ID |
