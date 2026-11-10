using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class TeddyBear : Product
{
    public string AgeRange { get; set; } = "0-3";
    public string Material { get; set; } = "Plush";

    public TeddyBear() { ProductType = "TeddyBear"; }
    public TeddyBear(int id, string name, int qty, double price, string supplier, string ageRange, string material)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange; Material = material;
        ProductType = "TeddyBear";
    }
}