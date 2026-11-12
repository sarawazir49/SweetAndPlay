# SweetAndPlay - Toys & Candy Store Management System

A console-based inventory management system for a Swedish toys and candy store. Built with **C# .NET 10**.

## Overview
Manages mixed inventory of toys and candy with automatic JSON persistence, color-coded stock alerts, and categorized reporting. Supports Swedish Krona (kr) pricing. Includes duplicate ID protection and search functionality.

## Main Features
- ✅ Add, Update, Delete, View Products
- ✅ **Search by ID** - Quick product lookup using GetProductById
- ✅ **Duplicate ID protection** - Blocks existing IDs, suggests GetNextAvailableId()
- ✅ Color-coded alerts (RED = low stock <5, YELLOW = expiry <30 days)
- ✅ Filtered Reports by Category with total value calculation
- ✅ Automatic save/load with `products.json`
- ✅ Hierarchical menu system (3 levels)
- ✅ Support for Barbie, HotWheels, Baby Box via CustomToy


        
## Product Types

| Type                | Age | Extra Field                     | Example |
|---------------------|-----|---------------------------------|---------|
| **Candy**           | -   | Flavor, Weight(g), ExpiryDate   | Haribo |
| **TeddyBear**       | 3+  | Material (Plush)                | Teddy |
| **Lego**            | 5+  | Pieces                          | Lego City |
| **PlaystationGame** | 10+ | Platform (PS5)                  | FIFA |
| **Tillbehor**       | 10+ | Category (Controller)           | PS Controller |
| **CustomToy**       | 4+  | CustomType, Description         | Barbie, HotWheels, Baby Box |

## Project Structure
SweetAndPlay/
├── Models/
│   ├── Product.cs (Base)
│   ├── Candy.cs
│   ├── Toy.cs
│   └── Toys/
│       ├── TeddyBear.cs
│       ├── Lego.cs
│       ├── PlaystationGame.cs
│       ├── Tillbehor.cs
│       └── CustomToy.cs 
├── Services/
│   └── Inventory.cs
├── Program.cs
└── products.json


## Color Coding System
- **RED** = Low quantity (Qty < 5) - High priority
- **YELLOW** = Expiry within 1 month (Candy)
- **WHITE** = Normal stock
- **CYAN** = Headers / Separators
- **GREEN** = Success messages & Total value

## Menu System

**Main Menu:** 
1. Add Product | 2. Update | 3. Delete | 4. View Products | 5. Generate Report | 6. Search by ID | 7. Exit

**Add Product:**
- Level 1: [1] Godis (Candy) leaf / [2] Leksaker (Toys)
- Level 2: [1] Teddy Bear / [2] Lego / [3] Digital Games / [4] Custom Toy (Barbie/HotWheels/Baby)
- Level 3: [1] Playstation Game / [2] Tillbehor

**Generate Report:**
- [1] Godis (Candy only)
- [2] Leksaker (Teddy + Lego + CustomToy) 
- [3] Digital Games (Playstation + Tillbehor only)
- [4] Alla Produkter (All)

## Inventory Methods

- `AddProduct(Product p)` - Blocks duplicate ID, suggests next available
- `GetNextAvailableId()` - Returns Max(ID)+1
- `GetProductById(int id)` - Find by ID, used for search & validation
- `DeleteProduct(int id)` - Delete + Save
- `UpdateProduct(...)` - Partial update (null = no change)
- `SaveToFile() / LoadFromFile()` - JSON persistence with type reconstruction
- `ViewProducts()` - Color-coded table view with age & expiry
- `GenerateReport()` - Filtered reports by category

## Technical Notes
- All classes inherit from Product base class
- ProductType property used for JSON type reconstruction
- Candy expiry handling: DateTime with default 30 days
- Duplicate protection: Checks GetProductById before adding

## Requirements
- .NET 10.0 SDK
- Visual Studio 2022/2026

