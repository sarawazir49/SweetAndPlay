namespace SweetAndPlay.Models.Toys;

public class Tillbehor : DigitalGame
{
    // Constructor for accessories (Swedish: Tillbehör)
    public Tillbehor(int id, string name, int qty, double price, string supplier, string ageRange, string category)
        : base(id, name, qty, price, supplier, ageRange)
    {
        Category = category;
    }

    // Accessory category (e.g., Controller, Headset, Charging Cable)
    public string Category { get; set; }

    // Product type identifier - used for JSON saving/loading and report filtering
    public override string ProductType => "Tillbehor";
}