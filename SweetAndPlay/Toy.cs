namespace SweetAndPlay.Models;

public class Toy : Product
{
    public string AgeGroup { get; set; } = "3+";
    public Toy() { ProductType = "Toy"; }
    public Toy(int id, string name, int qty, double price, string supplier, string ageGroup) : base(id, name, qty, price, supplier)
    { AgeGroup = ageGroup; ProductType = "Toy"; }
}