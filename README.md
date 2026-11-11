# SWEET & PLAY - Store System

Simple inventory management for candy and toys - C# Console App

## UML Design 

Product {abstract}
- ID, Name, Quantity, Price, Supplier, ProductType
- GetDetails() abstract

  Candy : Product - Flavor, Weight, ExpiryDate
  TeddyBear : Product - Material
  Lego : Product - Pieces
  PlaystationGame : Product - Platform
  Tillbehor : Product - Category

Inventory Service
- products: List<Product>
- AddProduct, GetProductById, DeleteProduct
- UpdateProduct(id, qty, price, name, supplier, flavor, expiry)
- SaveToFile, LoadFromFile, ViewProducts, GenerateReport

Program - Main() uses Inventory

## Features
- Add Product - 5 types
- Update Product - Name, Supplier, Qty, Price, Flavor, Expiry
- Delete Product
- View Products - RED<5, YELLOW<30d, WHITE OK
- Generate Report - Candy / Toys / All
- Auto Save to products.json

## How To Use
dotnet run

Menu:
1 Add Product
2 Update Product
3 Delete Product
4 View Products
5 Generate Report
6 Exit

## Classes and Methods
Product: ID, Name, Quantity, Price, Supplier, ProductType
Candy: Flavor, Weight, ExpiryDate
TeddyBear: Material
Lego: Pieces
PlaystationGame: Platform
Tillbehor: Category
Inventory: Add, Get, Delete, Update, Save, Load, View, Report

## Project Structure
Models/Product.cs, Candy.cs, TeddyBear.cs, Lego.cs, PlaystationGame.cs, Tillbehor.cs
Services/Inventory.cs
Program.cs

## Color Legend
RED = Low Qty<5
YELLOW = Expiry<30d
WHITE = OK

