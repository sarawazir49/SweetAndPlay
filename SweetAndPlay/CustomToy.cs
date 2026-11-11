namespace SweetAndPlay.Models.Toys;

public class CustomToy : Toy
{
    // Main constructor with all fields
    public CustomToy(int id, string name, int qty, double price, string supplier, string ageRange, string customType, string description)
        : base(id, name, qty, price, supplier, ageRange)
    {
        CustomType = customType;
        Description = description;
    }

    // Parameterless constructor required for deserialization - FIX FOR CS7036
    // Must call base with dummy values because Toy has no empty constructor
    public CustomToy() : base(0, "", 0, 0.0, "", "")
    {
        CustomType = "";
        Description = "";
    }

    public string CustomType { get; set; } = "";
    public string Description { get; set; } = "";

    public override string ProductType => "CustomToy";
}