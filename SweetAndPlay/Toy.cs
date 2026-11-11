namespace SweetAndPlay.Models;

public class Toy : Product
{
    // Constructor for generic toy base class
    public Toy(int id, string name, int qty, double price, string supplier, string ageRange)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange;
    }

    // Age recommendation (e.g., 3+, 5+, 10+) - defines target age group
    public string AgeRange { get; set; }

    // Product type identifier for saving/loading
    public override string ProductType => "Toy";
}