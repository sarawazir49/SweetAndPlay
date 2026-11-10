namespace SweetAndPlay.Models;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Supplier { get; set; } = "";
    public string ProductType { get; set; } = "Product";

    public Product() { }
    public Product(int id, string name, int qty, double price, string supplier)
    {
        ID = id; Name = name; Quantity = qty; Price = price; Supplier = supplier;
        ProductType = "Product";
    }
}