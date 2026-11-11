namespace SweetAndPlay.Models.Toys;

public class CustomToy : Toy
{
    // Main constructor
    public CustomToy(int id, string name, int qty, double price, string supplier, string ageRange, string customType, string description)
        : base(id, name, qty, price, supplier, ageRange)
    {
        CustomType = customType;
        Description = description;
    }


    public CustomToy() : base(0, "", 0, 0.0, "", "4+")
    {
        CustomType = "Barbie";
        Description = "";
    }

    public string CustomType { get; set; }
    public string Description { get; set; }

    public override string ProductType => "CustomToy";
}