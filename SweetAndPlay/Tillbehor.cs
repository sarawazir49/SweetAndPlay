using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class Tillbehor : Product
{
    public string AgeRange { get; set; } = "6+";
    public string Category { get; set; } = "Accessory";

    public Tillbehor() { ProductType = "Tillbehor"; }
    public Tillbehor(int id, string name, int qty, double price, string supplier, string ageRange, string category)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange; Category = category;
        ProductType = "Tillbehor";
    }
}