# EnverSoft Developer Assessment

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- MS-SQL Server (Express or higher)
- SQL Server Management Studio (SSMS) or `sqlcmd`

---

## Exercise 1 – List Set Operations

**Location:** `Exercise1/ConsoleApp1/`

Uses LINQ `Intersect` and `Except` to find common and unique elements between two integer arrays.

```bash
cd Exercise1/ConsoleApp1
dotnet run
```

Expected output:
```
3 4 5
1 2
6 7
Press <ENTER> to continue
```

---

## Exercise 2 – CSV Processor

**Location:** `Exercise2/`

Reads `Data.csv` and produces two output files:
- `name_frequencies.txt` — all first and last names by frequency (desc), then alphabetically (asc)
- `addresses_sorted.txt` — addresses sorted by street name (ignoring leading house number)

### Run the app
```bash
cd Exercise2/Exercise2.App
dotnet run
```

### Run unit tests
```bash
cd Exercise2
dotnet test
```

---

## Mini Project – Supplier App (3-Tier)

A web application for managing suppliers, built as a strict 3-tier architecture:

| Tier | Project | Technology |
|---|---|---|
| Frontend | `SupplierApp.Web` | ASP.NET Core Razor Pages + Bootstrap 5 |
| Services (middle) | `SupplierApp.API` → `Services/` | ASP.NET Core Web API |
| Repository + Database | `SupplierApp.API` → `Repository/` | Dapper + MS-SQL Server |

### Dependency flow
```
SuppliersController  →  ISupplierService  →  ISupplierRepository  →  SQL Server
     (Frontend)            (Services layer)       (Data layer)
```

The controller only knows about `ISupplierService`. The service layer handles business logic and delegates data access to `ISupplierRepository`. This keeps each tier independently replaceable and testable.

---

### 1. Create the database

Open SSMS or `sqlcmd` and run the script:
```
MiniProject/Database/CreateDatabase.sql
```
This creates the `SupplierApp` database, the `Suppliers` table, and seeds all 24 suppliers from `Supplier DB.xlsx`.

### 2. Configure the connection string

Edit `MiniProject/SupplierApp.API/appsettings.json` and set your SQL Server instance:
```json
{
  "ConnectionStrings": {
    "SupplierDb": "Server=YOUR_SERVER;Database=SupplierApp;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Run the API (terminal 1)
```bash
cd MiniProject/SupplierApp.API
dotnet run
```
- Base URL: `http://localhost:1400`
- Swagger UI: `http://localhost:1400/swagger` (opening `/` redirects here automatically)

Available endpoints:
| Method | URL | Description |
|---|---|---|
| `POST` | `/api/suppliers` | Add a new supplier |
| `GET` | `/api/suppliers/search?name=` | Search suppliers by company name |
| `GET` | `/api/suppliers/{id}` | Get a supplier by ID |

### 4. Run the Web frontend (terminal 2)
```bash
cd MiniProject/SupplierApp.Web
dotnet run
```

| Page | URL |
|---|---|
| Add Supplier | `http://localhost:5001/AddSupplier` |
| Search Supplier | `http://localhost:5001/SearchSupplier` |

---

## Project Structure
```
Assessment/
├── Exercise1/
│   └── ConsoleApp1/
│       └── Program.cs                  # LINQ Intersect / Except
├── Exercise2/
│   ├── Exercise2.App/
│   │   ├── Program.cs                  # CSV processor
│   │   └── Data.csv                    # Input file
│   ├── Exercise2.Tests/
│   │   └── CsvProcessorTests.cs        # xUnit tests
│   └── Exercise2.sln
└── MiniProject/
    ├── Database/
    │   └── CreateDatabase.sql          # DB + seed script
    ├── SupplierApp.API/
    │   ├── Controllers/
    │   │   └── SuppliersController.cs  # Injects ISupplierService
    │   ├── Services/
    │   │   ├── ISupplierService.cs     # Middle / services layer
    │   │   └── SupplierService.cs
    │   ├── Repository/
    │   │   ├── ISupplierRepository.cs  # Data access layer
    │   │   └── SupplierRepository.cs   # Dapper implementation
    │   ├── Models/
    │   │   └── Supplier.cs
    │   └── appsettings.json
    ├── SupplierApp.Web/
    │   └── Pages/
    │       ├── AddSupplier.cshtml      # Input screen
    │       └── SearchSupplier.cshtml   # Search screen
    └── SupplierApp.sln
```
