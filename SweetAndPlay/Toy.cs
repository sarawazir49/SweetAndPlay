using SweetAndPlay.Models;

namespace SweetAndPlay.Models;

public abstract class Toy : Product
{
    // Age recommendation (e.g., 3+, 5+, 10+)
    public string AgeRange { get; set; } = "6-12";

    public Toy() : base()
    {
    }

    // Constructor with params
    public Toy(int id, string name, int qty, double price, string supplier, string ageRange)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange;
    }

    // Product type
    public override string ProductType => "Toy";
}