namespace SweetAndPlay.Models;

public class Product
{
    // Unique product identifier
    public int ID { get; set; }

    // Product name
    public string Name { get; set; } = "";

    // Current stock quantity - used for low stock check (<5 = Red)
    public int Quantity { get; set; }

    // Unit price in SEK
    public double Price { get; set; }

    // Supplier name (e.g., Arla, Webhallen, Lego)
    public string Supplier { get; set; } = "";

    // Virtual property for product type - allows override in child classes
    // Enables polymorphic saving/loading and report filtering (Candy, Lego, etc.)
    public virtual string ProductType { get; set; } = "Product";

    // Default constructor for JSON deserialization
    public Product() { }

    // Full constructor for creating products
    public Product(int id, string name, int qty, double price, string supplier)
    {
        ID = id;
        Name = name;
        Quantity = qty;
        Price = price;
        Supplier = supplier;
        ProductType = "Product";
    }
}