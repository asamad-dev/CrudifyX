# CrudifyX: Full-Stack ASP.NET Core MVC + React Product Management System

## Project Overview
CrudifyX is a full-featured product management application built using **ASP.NET Core MVC**, **Entity Framework Core (Code First)**, **ASP.NET Identity**, **Dapper ORM (CURD)**, and **React + TypeScript**. The project combines backend practices (secure, validated, and modular) with a React-based frontend. This document explains setup steps, dependencies, and key design decisions.

---

### 1. Backend Development + Database

#### Authentication System
- Implemented using **ASP.NET Identity** with **Identity UI scaffolding**.
- Routes for login, logout, and register handled under `Areas/Identity/Pages/Account/`.
- `_Layout.cshtml` dynamically shows navigation based on `User.Identity.IsAuthenticated`.
- **Authorization** enforced using `[Authorize]` on `ProductController`, restricting CRUD actions to logged-in users.

Few Screenshots for ASP.NET Identity setup


![image](https://github.com/user-attachments/assets/58df4080-11ac-4ac4-91a5-f493643ba578)

![image](https://github.com/user-attachments/assets/f065fbf6-1a37-4aff-a2b6-d87afcf262b7)

![image](https://github.com/user-attachments/assets/7766dccd-4a75-4559-a232-e339e8116b7a)

![image](https://github.com/user-attachments/assets/6b712e94-3271-454f-be9f-99d005c1951a)

#### Code First with Seed Data
- `ApplicationDbContext.cs` uses Entity Framework Code First.
- Database seeded with 10 predefined products in `OnModelCreating()`.
- Price precision controlled using `.HasPrecision(10, 2)`.

As a result DB has following tables.

![image](https://github.com/user-attachments/assets/b4edb933-b696-4c6f-a989-d8a2ac7e3cd9)


#### Product CRUD Operations
- `Product.cs` includes:
  - `Id` (primary key)
  - `Name` (required, max 100 chars)
  - `Price` (required, decimal, > 0)
  - `Quantity` (required, int, ≥ 0)
- Server-side validation via `[Required]`, `[Range]`, and `ModelState.IsValid`.
- CRUD Views: `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`, and `Index.cshtml` inside `Views/Product`.
- Controller: `ProductController.cs` with both Razor (MVC) actions and API methods for React integration.

#### Dapper ORM Integration
- `ProductRepository.cs` handles DB access using `IDbConnection` injected via DI.
- SQL operations are parameterized, secure, and lightweight.

#### Stored Procedures
- MSSQL stored procedures used for all DB interactions:
  - `sp_GetAllProducts`
  - `sp_InsertProduct`
  - `sp_UpdateProduct`
  - `sp_DeleteProduct`
- Executed securely with Dapper, ensuring parameterized queries to prevent SQL injection.

Stored procedures in DB

![image](https://github.com/user-attachments/assets/6900d94e-51b7-49df-ad45-b6549c02dbfe)



### 2. Frontend Integration (React + TypeScript)

#### Setup
- `reactfrontend/` is a TypeScript + React app bundled with Webpack.
- Components:
  - `ProductSearch.tsx`: search + table + add/edit/delete
  - `index.tsx`: renders React component into `productSearchRoot`

#### Embedded in Razor
- React rendered inside `Views/Product/Index.cshtml`:

#### Functionality
- Fetches all products via `/api/Product/GetAll`
- Allows filtering by name with real-time input
- Full Create, Edit, Delete support via Fetch API
- Bootstrap used for layout and responsiveness
---

## Setup Instructions

### 1. Clone and Open
Use Visual Studio 2022+ and .NET 8 SDK.

### 2. Database Setup
- Create DB: `CREATE DATABASE CrudifyX;`
- Update `appsettings.json` connection string

### 3. Run Migrations
```powershell
Add-Migration "Message"
Update-Database
```

### 4. Add Stored Procedures in SSMS
```sql
CREATE PROCEDURE sp_GetAllProducts AS BEGIN SELECT * FROM Products END;
...
```

### 5. React Setup
```bash
cd reactfrontend
npm install
npx webpack
```
 
Alternate
```bash
cd reactfrontend
npm init -y
npm install react react-dom
npm install --save-dev typescript ts-loader @types/react @types/react-dom webpack webpack-cli webpack-dev-server html-webpack-plugin
npx webpack
```


---

## Project Tree (Summary)
```
CrudifyX.Web/
├── Areas/Identity/...       # Login/Register
├── Controllers/             # Product
├── DataAccess/              # ProductRepository (Dapper)
├── Models/Product.cs
├── Views/Product/*.cshtml   # Razor CRUD views
├── reactfrontend/           # React + TS frontend
├── wwwroot/js/bundle.js     # Compiled React app
```


## Demo
See `video.mp4` for a walkthrough of the entire app.

---

## Security & Best Practices
- Used `[ValidateAntiForgeryToken]` on all Razor forms
- Parameterized queries via Dapper to mitigate SQL injection
- Role-based access via `[Authorize]`
- Razor views only accessible to authenticated users
- Clean separation of concerns (Models, Repository, Controllers, Views)

---
