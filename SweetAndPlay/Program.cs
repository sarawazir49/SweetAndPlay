using SweetAndPlay.Models;
using SweetAndPlay.Models.Toys;
using SweetAndPlay.Services;

Inventory store = new Inventory();
store.LoadFromFile();

if (store.GetProductById(1) == null && store.GetProductById(2) == null)
{
    store.AddProduct(new TeddyBear(1, "Nalle Puh Teddy", 5, 199.99, "Scandic Toys AB", "0-3", "Plush"));
    store.AddProduct(new Lego(2, "Lego Star Wars 500pcs", 15, 599, "LEGO Supplier", "6-12", 500));
    store.AddProduct(new PlaystationGame(3, "FIFA 26 PS5", 8, 699, "Elgiganten AB", "7+", "PS5"));
    store.AddProduct(new Tillbehor(4, "PS5 Controller Pink", 3, 799, "Webhallen", "6+", "Controller"));
    store.AddProduct(new Candy(5, "Lösgodis Mix Sour", 20, 15.99, "Cloetta AB", "Sour Strawberry", 1000, DateTime.Now.AddDays(25)));
}

while (true)
{
    Console.Clear();

Console.ForegroundColor = ConsoleColor.Magenta;

Console.WriteLine(@"
  ************************************************************
  *                                                          *
  *              SWEET  AND  PLAY                             *
  *                                                          *
  *             Toys & Candy Store Management                 *
  *                                                          *
  ************************************************************
");

Console.ResetColor();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("                    MAIN MENU");
Console.WriteLine("  ------------------------------------------------");
Console.ResetColor();

Console.WriteLine("  1. Add Product");
Console.WriteLine("  2. Update Product");
Console.WriteLine("  3. Delete Product");
Console.WriteLine("  4. View Products");
Console.WriteLine("  5. Generate Report");
Console.WriteLine("  6. Exit");

Console.WriteLine("  ------------------------------------------------");
Console.Write("  Choose [1-6]: ");

string ch = Console.ReadLine() ?? "";

    if (ch == "4") { store.ViewProducts(); Console.WriteLine("\n  Tryck ENTER..."); Console.ReadLine(); }
    else if (ch == "5") { store.GenerateReport(); Console.WriteLine("\n  Tryck ENTER..."); Console.ReadLine(); }
    else if (ch == "6") { store.SaveToFile(); break; }
    else if (ch == "1")
    {
        Console.Clear();
        Console.WriteLine("\n--- ADD NEW PRODUCT ---");
        Console.WriteLine("1. Candy  2. TeddyBear  3. Lego  4. PlaystationGame  5. Tillbehor");
        Console.Write("Valj typ [1-5]: ");
        string t = Console.ReadLine() ?? "1";
        try
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Name: "); string name = Console.ReadLine() ?? "";
            Console.Write("Quantity: "); int qty = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Price (kr): "); double price = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Supplier: "); string sup = Console.ReadLine() ?? "Unknown";
            string age = "6+";

            if (t == "1")
            {
                Console.Write("Flavor: "); string flav = Console.ReadLine() ?? "Sweet";
                Console.Write("Weight (1000): "); int weight = int.TryParse(Console.ReadLine(), out int w) ? w : 1000;
                Console.Write("Expiry yyyy-MM-dd (tom=30d): "); string expStr = Console.ReadLine() ?? "";
                DateTime exp = string.IsNullOrWhiteSpace(expStr) ? DateTime.Now.AddDays(30) : DateTime.Parse(expStr);
                store.AddProduct(new Candy(id, name, qty, price, sup, flav, weight, exp));
            }
            else if (t == "2")
            {
                Console.Write("Material (Plush): "); string mat = Console.ReadLine() ?? "Plush";
                store.AddProduct(new TeddyBear(id, name, qty, price, sup, age, mat));
            }
            else if (t == "3")
            {
                Console.Write("Pieces: "); int pieces = int.TryParse(Console.ReadLine(), out int p) ? p : 500;
                store.AddProduct(new Lego(id, name, qty, price, sup, age, pieces));
            }
            else if (t == "4")
            {
                Console.Write("Platform (PS5): "); string plat = Console.ReadLine() ?? "PS5";
                store.AddProduct(new PlaystationGame(id, name, qty, price, sup, age, plat));
            }
            else
            {
                Console.Write("Category: "); string cat = Console.ReadLine() ?? "Accessory";
                store.AddProduct(new Tillbehor(id, name, qty, price, sup, age, cat));
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✅ Tillagd och sparad! {name}"); Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ❌ Fel: {ex.Message}"); Console.ResetColor();
        }
        Console.WriteLine("\n  Tryck ENTER..."); Console.ReadLine();
    }
    else if (ch == "2")
    {
        Console.Clear();
        Console.WriteLine("\n--- UPDATE PRODUCT ---");
        store.ViewProducts();
        Console.Write("\nAnge ID att uppdatera: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var existing = store.GetProductById(id);
            if (existing == null)
            {
                Console.WriteLine("\n  ❌ Hittades inte!");
            }
            else
            {
                Console.WriteLine($"\n  Nuvarande: {existing.Name} | {existing.Supplier} | Qty:{existing.Quantity} | {existing.Price}kr");
                Console.WriteLine("  (Lämna tomt = ingen ändring)\n");

                Console.Write("Nytt Namn: ");
                string nNameStr = Console.ReadLine() ?? "";
                string? nName = string.IsNullOrWhiteSpace(nNameStr) ? null : nNameStr;

                Console.Write("Ny Supplier: ");
                string nSupStr = Console.ReadLine() ?? "";
                string? nSup = string.IsNullOrWhiteSpace(nSupStr) ? null : nSupStr;

                Console.Write("Ny Quantity: ");
                string qStr = Console.ReadLine() ?? "";
                int? newQty = null;
                if (!string.IsNullOrWhiteSpace(qStr) && int.TryParse(qStr, out int qVal)) newQty = qVal;

                Console.Write("Nytt Price kr: ");
                string pStr = Console.ReadLine() ?? "";
                double? newPrice = null;
                if (!string.IsNullOrWhiteSpace(pStr) && double.TryParse(pStr, out double pVal)) newPrice = pVal;

                // Iza Candy - flavor kamen
                string? nFlavor = null;
                if (existing is Candy)
                {
                    Console.Write("Ny Flavor (Candy): ");
                    string fStr = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(fStr)) nFlavor = fStr;
                }

                bool ok = store.UpdateProduct(id, newQty, newPrice, nName, nSup, nFlavor);
                if (ok)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    var updated = store.GetProductById(id);
                    Console.WriteLine($"\n  ✅ Uppdaterad och sparad! {updated?.Name} | {updated?.Supplier}");
                    Console.ResetColor();
                }
                else Console.WriteLine("\n  ❌ Fel vid uppdatering!");
            }
        }
        Console.WriteLine("\n  Tryck ENTER..."); Console.ReadLine();
    }

    else if (ch == "3")
    {
        Console.Clear();
        Console.WriteLine("\n--- DELETE PRODUCT ---");
        store.ViewProducts();
        Console.Write("\nAnge ID att ta bort: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write($"Är du säker att ta bort ID {id}? (j/n): ");
            string conf = Console.ReadLine() ?? "n";
            if (conf.ToLower() == "j" || conf.ToLower() == "ja" || conf.ToLower() == "y")
            {
                bool ok = store.DeleteProduct(id);
                Console.WriteLine(ok ? $"\n  ✅ Borttagen och sparad! ID {id}" : "\n  ❌ Hittades inte!");
            }
        }
        Console.WriteLine("\n  Tryck ENTER..."); Console.ReadLine();
    }
}