namespace SweetAndPlay.Models.Toys;

public class PlaystationGame : DigitalGame
{
    // Constructor for PlayStation games
    public PlaystationGame(int id, string name, int qty, double price, string supplier, string ageRange, string platform)
        : base(id, name, qty, price, supplier, ageRange)
    {
        Platform = platform;
    }

    // Gaming platform (e.g., PS5, PS4, PS3) - for filtering and display
    public string Platform { get; set; }

    // Product type identifier - used for JSON saving/loading and report filtering
    public override string ProductType => "PlaystationGame";
}