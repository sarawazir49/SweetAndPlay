using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class TeddyBear : Product
{
    // Age recommendation for Teddy Bear (e.g., 3+, 0-3)
    public string AgeRange { get; set; } = "0-3";

    // Material type (e.g., Plush, Cotton)
    public string Material { get; set; } = "Plush";

    // Default constructor
    public TeddyBear() { ProductType = "TeddyBear"; }

    // Full constructor for Teddy Bear
    public TeddyBear(int id, string name, int qty, double price, string supplier, string ageRange, string material)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange;
        Material = material;
        ProductType = "TeddyBear";
    }
}