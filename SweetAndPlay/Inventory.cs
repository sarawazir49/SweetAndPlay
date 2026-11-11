using SweetAndPlay.Models;
using SweetAndPlay.Models.Toys;
using System.Text.Json;

namespace SweetAndPlay.Services;

public class Inventory
{
    // In-memory product list and file path for persistence
    private List<Product> products = new();
    private string filePath = "products.json";

    public void AddProduct(Product p)
    {
        var old = products.FirstOrDefault(x => x.ID == p.ID);
        if (old != null) products.Remove(old);
        products.Add(p);
        SaveToFile();
    }

    // Get product by ID, null if not found
    public Product? GetProductById(int id) => products.FirstOrDefault(x => x.ID == id);

    // Delete product by ID
    public bool DeleteProduct(int id)
    {
        var p = products.FirstOrDefault(x => x.ID == id);
        if (p != null) { products.Remove(p); SaveToFile(); return true; }
        return false;
    }

    // Update product fields - only updates provided values
    public bool UpdateProduct(int id, int? newQty, double? newPrice, string? newName = null, string? newSupplier = null, string? newFlavor = null, DateTime? newExpiry = null)
    {
        var p = products.FirstOrDefault(x => x.ID == id);
        if (p == null) return false;

        if (newQty.HasValue) p.Quantity = newQty.Value;
        if (newPrice.HasValue) p.Price = newPrice.Value;
        if (!string.IsNullOrWhiteSpace(newName)) p.Name = newName;
        if (!string.IsNullOrWhiteSpace(newSupplier)) p.Supplier = newSupplier;

        // Candy-specific updates
        if (p is Candy cand)
        {
            if (!string.IsNullOrWhiteSpace(newFlavor)) cand.Flavor = newFlavor;
            if (newExpiry.HasValue) cand.ExpiryDate = newExpiry.Value;
        }

        SaveToFile();
        return true;
    }

    // Save product list to JSON file
    public void SaveToFile()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var objList = products.Select(x => (object)x).ToList();
            File.WriteAllText(filePath, JsonSerializer.Serialize(objList, options));
        }
        catch (Exception ex) { Console.WriteLine($"Save error: {ex.Message}"); }
    }

    // Load products from JSON and reconstruct correct concrete types
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
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "3+" : "3+";
                    string mat = el.TryGetProperty("Material", out var m) ? m.GetString() ?? "Plush" : "Plush";
                    products.Add(new TeddyBear(id, name, qty, price, sup, age, mat));
                }
                else if (type.Contains("Lego"))
                {
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "5+" : "5+";
                    int pieces = el.TryGetProperty("Pieces", out var pc) ? pc.GetInt32() : 500;
                    products.Add(new Lego(id, name, qty, price, sup, age, pieces));
                }
                else if (type.Contains("Playstation"))
                {
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "10+" : "10+";
                    string plat = el.TryGetProperty("Platform", out var pl) ? pl.GetString() ?? "PS5" : "PS5";
                    products.Add(new PlaystationGame(id, name, qty, price, sup, age, plat));
                }
                else if (type.Contains("CustomToy") || el.TryGetProperty("CustomType", out _) || type.Contains("Barbie") || type.Contains("HotWheels") || type.Contains("Baby"))
                {
                    // CustomToy - also handles legacy files where ProductType was Barbie / Baby box etc.
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "4+" : "4+";
                    string customType = el.TryGetProperty("CustomType", out var ct) ? ct.GetString() ?? type : type;
                    if (string.IsNullOrWhiteSpace(customType) || customType == "CustomToy") customType = "Barbie";
                    string desc = el.TryGetProperty("Description", out var d) ? d.GetString() ?? "" : "";
                    products.Add(new CustomToy(id, name, qty, price, sup, age, customType, desc));
                }
                else
                {
                    // Fallback to Tillbehor (Accessory)
                    string age = el.TryGetProperty("AgeRange", out var a) ? a.GetString() ?? "10+" : "10+";
                    string cat = el.TryGetProperty("Category", out var c) ? c.GetString() ?? "Accessory" : "Accessory";
                    products.Add(new Tillbehor(id, name, qty, price, sup, age, cat));
                }
            }
        }
        catch { }
    }

    // Display all products in formatted table with color coding
    public void ViewProducts()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n--- VIEW {products.Count} produkter ---");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("ID | Name           | Qty | Price  | Type       | Supplier | Age  | Material/Category      | Expiry     | Flavor");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
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

            string age = "-";
            string extra = "-";
            if (p is TeddyBear ted) { age = ted.AgeRange; extra = ted.Material; }
            else if (p is Lego lego) { age = lego.AgeRange; extra = $"{lego.Pieces} pcs"; }
            else if (p is PlaystationGame ps) { age = ps.AgeRange; extra = ps.Platform; }
            else if (p is Tillbehor tb) { age = tb.AgeRange; extra = tb.Category; }
            else if (p is CustomToy custom) { age = custom.AgeRange; extra = custom.CustomType; }
            else if (p is Candy candyView) { age = "-"; extra = candyView.Flavor; }

            extra = extra.Length > 20 ? extra.Substring(0, 20) : extra.PadRight(20);

            if (p is Candy cand)
            {
                string exp = cand.ExpiryDate.ToString("yyyy-MM-dd");
                string flav = cand.Flavor.Length > 10 ? cand.Flavor.Substring(0, 10) : cand.Flavor;
                Console.WriteLine($"{p.ID,-2} | {name} | {p.Quantity,-3} | {p.Price,5:F0}kr | {type} | {sup} | {age,-4} | {extra} | {exp} | {flav}");
            }
            else
            {
                Console.WriteLine($"{p.ID,-2} | {name} | {p.Quantity,-3} | {p.Price,5:F0}kr | {type} | {sup} | {age,-4} | {extra} | N/A        | -");
            }
            Console.ResetColor();
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("RED=Low<5  YELLOW=Expiry<30d | Age: Teddy 3+, Lego 5+, PS 10+, Tillbehör 10+");
        Console.ResetColor();
    }

    // Generate filtered reports with whole-line color coding
    public void GenerateReport()
    {
        Console.Clear();
        Console.WriteLine("\n---------- GENERATE REPORT MENU ----------");
        Console.WriteLine(" [1] Godis (Candy)");
        Console.WriteLine(" [2] Leksaker (Teddy + Lego + Barbie)");
        Console.WriteLine(" [3] Digital Games (Playstation + Tillbehör)");
        Console.WriteLine(" [4] Alla Produkter");
        Console.WriteLine("------------------------------------------");
        Console.Write("\n  Valj [1-4]: ");
        string ch = Console.ReadLine() ?? "4";

        List<Product> list;
        if (ch == "1") list = products.Where(p => p is Candy).ToList();
        else if (ch == "2") list = products.Where(p => p is TeddyBear || p is Lego || p is CustomToy).ToList();
        else if (ch == "3") list = products.Where(p => p is PlaystationGame || p is Tillbehor).ToList();
        else list = products.ToList();

        string title = ch == "1" ? "GODIS REPORT" : ch == "2" ? "LEKSAKER REPORT" : ch == "3" ? "DIGITAL GAMES REPORT" : "ALLA PRODUKTER REPORT";
        string line = new string('-', 130);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n--- {title} ---");
        Console.WriteLine($"Totalt Varde: {list.Sum(x => x.Quantity * x.Price):F2} kr | Antal: {list.Count}");
        Console.WriteLine(line);
        Console.WriteLine($"{"ID",-3} | {"Name",-16} | {"Age",-3} | {"Material/Pieces",-20} | {"Supplier",-14} | {"Expiry",-10} | {"Price",-7} | {"Qty",-3} | {"Total",-8} | Status");
        Console.WriteLine(line);
        Console.ResetColor();

        foreach (var p in list.OrderBy(x => x.ID))
        {
            string age = "All";
            string extra = "-";
            string expiry = "N/A";
            bool isCandy = false;
            bool isNearExpiry = false;

            if (p is TeddyBear ted) { age = ted.AgeRange; extra = $"Mat: {ted.Material}"; }
            else if (p is Lego lego) { age = lego.AgeRange; extra = $"{lego.Pieces} pcs"; }
            else if (p is CustomToy cus) { age = cus.AgeRange; extra = cus.CustomType; }
            else if (p is PlaystationGame ps) { age = ps.AgeRange; extra = ps.Platform; }
            else if (p is Tillbehor tb) { age = tb.AgeRange; extra = tb.Category; }
            else if (p is Candy candy)
            {
                isCandy = true;
                expiry = candy.ExpiryDate.ToString("yyyy-MM-dd");
                extra = candy.Flavor;
                if (candy.ExpiryDate <= DateTime.Now.AddMonths(1))
                    isNearExpiry = true;
            }

            string name = p.Name.Length > 16 ? p.Name.Substring(0, 16) : p.Name.PadRight(16);
            string extraF = extra.Length > 20 ? extra.Substring(0, 20) : extra.PadRight(20);
            string sup = p.Supplier.Length > 14 ? p.Supplier.Substring(0, 14) : p.Supplier.PadRight(14);
            string ageF = age.PadRight(3);

            bool low = p.Quantity < 5;

            // Color logic: Red = low qty (priority), Yellow = expiry < 1 month
            if (low) Console.ForegroundColor = ConsoleColor.Red;
            else if (isNearExpiry) Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.White;

            string expiryDisplay = isCandy ? expiry : "N/A";

            Console.WriteLine($"{p.ID,-3} | {name} | {ageF} | {extraF} | {sup} | {expiryDisplay,-10} | {p.Price,5:F0}kr | {p.Quantity,-3} | {p.Quantity * p.Price,6:F0}kr | {(low ? "LOW" : isNearExpiry ? "EXPIRY!" : "OK")}");
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(line);
        Console.ResetColor();

        // Legend
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("RED = LOW QTY (<5)  ");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("YELLOW = EXPIRY < 1 MONTH");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"TOTALT VARDE: {list.Sum(x => x.Quantity * x.Price):F2} kr | ANTAL: {list.Count}");
        Console.ResetColor();

        Console.WriteLine("\nTryck ENTER...");
        Console.ReadLine();
    }
}