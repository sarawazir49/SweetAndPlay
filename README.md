
# SweetAndPlay - Toys & Candy Store Management System

A console-based inventory management system for managing toys and candy products. Built with C# .NET.

## Overview
This application manages a mixed inventory of toys and candy with automatic JSON persistence, color-coded stock alerts, and categorized reporting.

## Main Features
- Add, Update, Delete, View Products
- Color-coded alerts (Red for low stock, Yellow for near expiry)
- Filtered Reports by Category
- Automatic save/load with products.json
- Hierarchical menu system for product creation

## Product Types
- *Candy* - Flavor, Weight (g), ExpiryDate
- *TeddyBear* - AgeRange 3+, Material (Plush)
- *Lego* - AgeRange 5+, Pieces count
- *PlaystationGame* - AgeRange 10+, Platform (PS5)
- *Tillbehor* - AgeRange 10+, Category (Controller)
- *CustomToy* - For Barbie, HotWheels, Cars - Custom age

## Project Structure
SweetAndPlay/
├── Models/
│   ├── Product.cs
│   ├── Candy.cs
│   ├── Toy.cs
│   └── Toys/
│       ├── TeddyBear.cs
│       ├── Lego.cs
│       ├── DigitalGame.cs
│       ├── PlaystationGame.cs
│       ├── Tillbehor.cs
│       └── CustomToy.cs
├── Services/
│   └── Inventory.cs
├── Program.cs
└── products.json
## Color Coding System
- *RED* = Low quantity (Qty < 5) - High priority
- *YELLOW* = Expiry within 1 month (Candy) - Whole line yellow
- *WHITE* = Normal stock
- *GREEN* = Total value summary

## Menu System
*Main Menu:* Add Product, Update Product, Delete Product, View Products, Generate Report, Exit

*Add Product:*
- Menu 1: [1] Godis (Candy) / [2] Leksaker (Toys)
- Menu 2: [1] Teddy Bear / [2] Lego / [3] Digital Games / [4] Custom Toy
- Menu 3: [1] Playstation Game / [2] Tillbehor

*Generate Report:*
- [1] Godis (Candy only)
- [2] Leksaker (Teddy + Lego + CustomToy)
- [3] Digital Games (Playstation + Tillbehor)
- [4] Alla Produkter (All)

## How To Run
dotnet run
- Requires .NET 6+
- First run creates 5 sample products if empty
- Auto-saves to products.json

## Inventory Methods
- `AddProduct()` - Add or replace by ID
- `GetProductById()` - Get by ID
- `DeleteProduct()` - Delete with save
- `UpdateProduct()` - Partial update (null = no change)
- `SaveToFile()` / `LoadFromFile()` - JSON persistence
- `ViewProducts()` - Color-coded table
- `GenerateReport()` - Filtered reports

## Notes
- All classes inherit from Product base class
- ProductType used for JSON type reconstruction
- Price in Swedish Krona (kr)
- Supplier tracked for all products

---
Copy hayda kolo w 7otto bi GitHub README.md - byizbat 100%
