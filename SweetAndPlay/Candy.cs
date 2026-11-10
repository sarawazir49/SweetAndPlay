namespace SweetAndPlay.Models;

public class Candy : Product
{
    public string Flavor { get; set; } = "Sweet";
    public int Weight { get; set; } = 1000;
    public DateTime ExpiryDate { get; set; }

    public Candy() { ProductType = "Candy"; }
    public Candy(int id, string name, int qty, double price, string supplier, string flavor, int weight, DateTime expiry)
        : base(id, name, qty, price, supplier)
    {
        Flavor = flavor; Weight = weight; ExpiryDate = expiry;
        ProductType = "Candy";
    }
    public int DaysUntilExpiry() => (int)(ExpiryDate - DateTime.Now).TotalDays;
}