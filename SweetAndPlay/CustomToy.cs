namespace SweetAndPlay.Models.Toys;

public class CustomToy : Toy
{
    // Constructor for creating custom toys like Barbie, HotWheels, etc.
    public CustomToy(int id, string name, int qty, double price, string supplier, string ageRange, string customType, string description)
        : base(id, name, qty, price, supplier, ageRange)
    {
        CustomType = customType;
        Description = description;
    }

    // Type of custom toy (e.g., Barbie, HotWheels, Squishy) - used for categorization
    public string CustomType { get; set; }

    // Additional description for the toy (e.g., material, features)
    public string Description { get; set; }

    // Overrides ProductType to return custom type name instead of generic "Toy"
    // This allows report to show "Barbie" or "HotWheels" as product type
    public override string ProductType => CustomType;
}