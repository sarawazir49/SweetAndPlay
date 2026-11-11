using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class DigitalGame : Product
{
    // Constructor for digital games base class
    // Used as parent for PlaystationGame and Tillbehor
    public DigitalGame(int id, string name, int qty, double price, string supplier, string ageRange)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange;
    }

    // Age recommendation (e.g., 3+, 10+, 18+) - required for both games and accessories
    public string AgeRange { get; set; }

    // Product type identifier for saving/loading - distinguishes digital from physical toys
    public override string ProductType => "DigitalGame";
}