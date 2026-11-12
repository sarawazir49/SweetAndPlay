using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class Lego : Toy
{
    public int Pieces { get; set; } = 500;

   
    public Lego() : base()
    {
        AgeRange = "6-12";
    }

    
    public Lego(int id, string name, int qty, double price, string supplier, string ageRange, int pieces)
        : base(id, name, qty, price, supplier, ageRange)
    {
        Pieces = pieces;
    }

    public override string ProductType => "Lego";
}