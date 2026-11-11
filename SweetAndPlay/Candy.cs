namespace SweetAndPlay.Models;

public class Candy : Product
{
    // Flavor of the candy (e.g., Sour, Sweet, Caramel)
    public string Flavor { get; set; } = "Sweet";

    // Weight in grams
    public int Weight { get; set; } = 1000;

    // Expiration date for the candy - used for Yellow warning if < 1 month
    public DateTime ExpiryDate { get; set; }

    // Default constructor - sets ProductType
    public Candy() { ProductType = "Candy"; }

    // Full constructor for creating candy with all properties
    public Candy(int id, string name, int qty, double price, string supplier, string flavor, int weight, DateTime expiry)
        : base(id, name, qty, price, supplier)
    {
        Flavor = flavor;
        Weight = weight;
        ExpiryDate = expiry;
        ProductType = "Candy";
    }

    // Calculates days remaining until expiry - used to check if < 30 days
    public int DaysUntilExpiry() => (int)(ExpiryDate - DateTime.Now).TotalDays;
}