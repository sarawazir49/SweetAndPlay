using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class Lego : Product
{
    public string AgeRange { get; set; } = "6-12";
    public int Pieces { get; set; } = 500;

    public Lego() { ProductType = "Lego"; }
    public Lego(int id, string name, int qty, double price, string supplier, string ageRange, int pieces)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange; Pieces = pieces;
        ProductType = "Lego";
    }
}