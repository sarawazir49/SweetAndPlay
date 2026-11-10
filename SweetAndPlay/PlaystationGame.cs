using SweetAndPlay.Models;

namespace SweetAndPlay.Models.Toys;

public class PlaystationGame : Product
{
    public string AgeRange { get; set; } = "7+";
    public string Platform { get; set; } = "PS5";

    public PlaystationGame() { ProductType = "PlaystationGame"; }
    public PlaystationGame(int id, string name, int qty, double price, string supplier, string ageRange, string platform)
        : base(id, name, qty, price, supplier)
    {
        AgeRange = ageRange; Platform = platform;
        ProductType = "PlaystationGame";
    }
}