using SweetAndPlay.Models;
using SweetAndPlay.Models.Toys;
using System.Text.Json;

namespace SweetAndPlay.Services;

public class Inventory
{
    private List<Product> products = new();
    private string filePath = "products.json";

    public void AddProduct(Product p)
    {
        var old = products.FirstOrDefault(x => x.ID == p.ID);
        if (old != null) products.Remove(old);
        products.Add(p);
        SaveToFile();
    }

    public Product? GetProductById(int id) => products.FirstOrDefault(x => x.ID == id);

    public bool DeleteProduct(int id)
    {
        var p = products.FirstOrDefault(x => x.ID == id);
        if (p != null) { products.Remove(p); SaveToFile(); return true; }
        return false;
    }

    // ===== UPDATE JDIDE - FIHA NAME + SUPPLIER + PRICE + QTY =====
    public bool UpdateProduct(int id, int? newQty, double? newPrice, string? newName = null, string? newSupplier = null, string? newFlavor = null, DateTime? newExpiry = null)
    {
        var p = products.FirstOrDefault(x => x.ID == id);
        if (p == null) return false;

        if (newQty.HasValue) p.Quantity = newQty.Value;
        if (newPrice.HasValue) p.Price = newPrice.Value;
        if (!string.IsNullOrWhiteSpace(newName)) p.Name = newName;
        if (!string.IsNullOrWhiteSpace(newSupplier)) p.Supplier = newSupplier;

        // Iza Candy - fi flavor w expiry kamen
        if (p is Candy cand)
        {
            if (!string.IsNullOrWhiteSpace(newFlavor)) cand.Flavor = newFlavor;
            if (newExpiry.HasValue) cand.ExpiryDate = newExpiry.Value;
        }

        SaveToFile();
        return true;
    }

    public void SaveToFile()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var objList = products.Select(x => (object)x).ToList();
            File.WriteAllText(filePath, JsonSerializer.Serialize(objList, options));
        }
        catch (Exception ex) { Console.WriteLine($"Save fel: {ex.Message}"); }
    }

    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(filePath)) return;
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json) || json.Trim() == "[]") return;
            var doc = JsonDocument.Parse(json);
            products.Clear();
            foreach (var el in doc.RootElement.EnumerateArray())
            {
                string type = el.TryGetProperty("ProductType", out var t) ? t.GetString() ?? "" : "";
                int id = el.GetProperty("ID").GetInt32();
                string name = el.GetProperty("Name").GetString() ?? "";
                int qty = el.GetProperty("Quantity").GetInt32();
                double price = el.GetProperty("Price").GetDouble();
                string sup = el.GetProperty("Supplier").GetString() ?? "";

                if (type.Contains("Candy"))
                {
                    string flav = el.TryGetProperty("Flavor", out var f) ? f.GetString() ?? "Sweet" : "Sweet";
                    int weight = el.TryGetProperty("Weight", out var w) ? w.GetInt32() : 1000;
                    DateTime exp = el.TryGetProperty("ExpiryDate", out var e) ? e.GetDateTime() : DateTime.Now.AddMonths(1);
                    products.Add(new Candy(id, name, qty, price, sup, flav, weight, exp));
                }
                else if (type.Contains("Teddy"))
                {
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "6+" : "6+";
                    string mat = el.TryGetProperty("Material", out var m) ? m.GetString() ?? "Plush" : "Plush";
                    products.Add(new TeddyBear(id, name, qty, price, sup, age, mat));
                }
                else if (type.Contains("Lego"))
                {
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "6+" : "6+";
                    int pieces = el.TryGetProperty("Pieces", out var pc) ? pc.GetInt32() : 500;
                    products.Add(new Lego(id, name, qty, price, sup, age, pieces));
                }
                else if (type.Contains("Playstation"))
                {
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "6+" : "6+";
                    string plat = el.TryGetProperty("Platform", out var pl) ? pl.GetString() ?? "PS5" : "PS5";
                    products.Add(new PlaystationGame(id, name, qty, price, sup, age, plat));
                }
                else
                {
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "6+" : "6+";
                    string cat = el.TryGetProperty("Category", out var c) ? c.GetString() ?? "Accessory" : "Accessory";
                    products.Add(new Tillbehor(id, name, qty, price, sup, age, cat));
                }
            }
        }
        catch { }
    }

    public void ViewProducts()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n--- VIEW {products.Count} produkter ---");
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("ID | Name           | Qty | Price  | Type       | Supplier | Expiry     | Flavor");
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.ResetColor();
        foreach (var p in products.OrderBy(x => x.ID))
        {
            bool low = p.Quantity < 5;
            bool expSoon = p is Candy c && (c.ExpiryDate - DateTime.Now).TotalDays <= 30;
            if (low && expSoon) Console.ForegroundColor = ConsoleColor.Red;
            else if (expSoon) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (low) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.White;

            string name = p.Name.Length > 14 ? p.Name.Substring(0, 14) : p.Name.PadRight(14);
            string sup = p.Supplier.Length > 8 ? p.Supplier.Substring(0, 8) : p.Supplier.PadRight(8);
            string type = p.ProductType.Length > 10 ? p.ProductType.Substring(0, 10) : p.ProductType.PadRight(10);
            if (p is Candy cand)
            {
                string exp = cand.ExpiryDate.ToString("yyyy-MM-dd");
                string flav = cand.Flavor.Length > 10 ? cand.Flavor.Substring(0, 10) : cand.Flavor;
                Console.WriteLine($"{p.ID,-2} | {name} | {p.Quantity,-3} | {p.Price,5:F0}kr | {type} | {sup} | {exp} | {flav}");
            }
            else
            {
                Console.WriteLine($"{p.ID,-2} | {name} | {p.Quantity,-3} | {p.Price,5:F0}kr | {type} | {sup} | N/A        | -");
            }
            Console.ResetColor();
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("RED=Low<5  YELLOW=Expiry<30d");
        Console.ResetColor();
    }

    public void GenerateReport()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n========== GENERATE REPORT MENU ==========");
        Console.WriteLine(" [1] Godis (Candy)");
        Console.WriteLine(" [2] Leksaker (Toys)");
        Console.WriteLine(" [3] Alla Produkter");
        Console.WriteLine("==========================================");
        Console.ResetColor();
        Console.Write("\n  Valj [1-3]: ");
        string ch = Console.ReadLine() ?? "3";

        var list = ch == "1" ? products.Where(p => p is Candy).ToList()
                 : ch == "2" ? products.Where(p => p is not Candy).ToList()
                 : products.ToList();

        string title = ch == "1" ? "GODIS REPORT" : ch == "2" ? "LEKSAKER REPORT" : "ALLA PRODUKTER REPORT";
        double totalValue = list.Sum(x => x.Quantity * x.Price);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n--- {title} ---");
        Console.WriteLine($"Totalt Varde: {totalValue:F2} kr | Antal: {list.Count}");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("ID | Name           | Supplier | Expiry     | Price  | Qty | Total   | Status");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.ResetColor();

        foreach (var p in list.OrderBy(x => x.ID))
        {
            bool low = p.Quantity < 5;
            bool expSoon = p is Candy c && (c.ExpiryDate - DateTime.Now).TotalDays <= 30;
            if (low && expSoon) Console.ForegroundColor = ConsoleColor.Red;
            else if (expSoon) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (low) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.White;

            string name = p.Name.Length > 14 ? p.Name.Substring(0, 14) : p.Name.PadRight(14);
            string sup = p.Supplier.Length > 8 ? p.Supplier.Substring(0, 8) : p.Supplier.PadRight(8);
            double itemTotal = p.Quantity * p.Price;
            string status = "OK";
            if (low) status = "LOW";
            if (expSoon) status = status == "LOW" ? "LOW+EXP" : "EXP SOON";

            if (p is Candy cand)
            {
                string exp = cand.ExpiryDate.ToString("yyyy-MM-dd");
                Console.WriteLine($"{p.ID,-2} | {name} | {sup} | {exp} | {p.Price,5:F0}kr | {p.Quantity,3} | {itemTotal,6:F0}kr | {status}");
            }
            else
            {
                Console.WriteLine($"{p.ID,-2} | {name} | {sup} | N/A        | {p.Price,5:F0}kr | {p.Quantity,3} | {itemTotal,6:F0}kr | {status}");
            }
            Console.ResetColor();
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("-----------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"TOTALT VARDE: {totalValue:F2} kr | ANTAL: {list.Count}");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("RED=Low<5  YELLOW=Expiry<30d");
        Console.ResetColor();
    }
}