using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class TeddyBear : Toy
{
    // Material type (e.g., Plush, Cotton)
    public string Material { get; set; } = "Plush";

    // Default constructor for JSON
    public TeddyBear() : base()
    {
        AgeRange = "0-3";
    }

    // Full constructor for Teddy Bear
    public TeddyBear(int id, string name, int qty, double price, string supplier, string ageRange, string material)
        : base(id, name, qty, price, supplier, ageRange)
    {
        Material = material;
    }

    public override string ProductType => "TeddyBear";
}