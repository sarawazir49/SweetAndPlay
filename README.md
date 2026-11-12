# SweetAndPlay - Toys & Candy Store Management System

A console-based inventory management system for a Swedish toys and candy store. Built with **C# .NET 10**.

## Overview
Manages mixed inventory of toys and candy with automatic JSON persistence, color-coded stock alerts, and categorized reporting. Supports Swedish Krona (kr) pricing.

## Main Features
- ✅ Add, Update, Delete, View Products
- ✅ Color-coded alerts (RED = low stock <5, YELLOW = expiry <30 days)
- ✅ Filtered Reports by Category
- ✅ Automatic save/load with `products.json`
- ✅ Hierarchical menu system
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
- **YELLOW** = Expiry within 1 month (Candy) - Whole line yellow
- **WHITE** = Normal stock
- **CYAN** = Headers / Separators
- **GREEN** = Total value summary

## Menu System

**Main Menu:** Add | Update | Delete | View Products | Generate Report | Exit

**Add Product:**
- Level 1: [1] Godis (Candy) / [2] Leksaker (Toys)
- Level 2: [1] Teddy Bear / [2] Lego / [3] Digital Games / [4] Custom Toy (Barbie/HotWheels/Baby)
- Level 3: [1] Playstation Game / [2] Tillbehor

**Generate Report:**
- [1] Godis (Candy only)
- [2] Leksaker (Teddy + Lego + CustomToy) 
- [3] Digital Games (Playstation + Tillbehor only)
- [4] Alla Produkter (All)

## Inventory Methods

- `AddProduct(Product p)` - Add by ID + Save
- `GetProductById(int id)` - Find by ID
- `DeleteProduct(int id)` - Delete + Save
- `UpdateProduct(...)` - Partial update (null = no change)
- `SaveToFile() / LoadFromFile()` - JSON persistence with type reconstruction
- `ViewProducts()` - Color-coded table view
- `GenerateReport()` - Filtered reports with color logic

## Technical Notes
All classes inherit from Product base class.
ProductType property used for JSON type reconstruction.

## Requirements
.NET 10.0 SDK
Visual Studio 2022/2026
