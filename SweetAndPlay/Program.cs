using SweetAndPlay.Models;
using SweetAndPlay.Models.Toys;
using SweetAndPlay.Services;

Inventory store = new Inventory();
store.LoadFromFile();

// Seed data only on first run - checks if IDs 1 and 2 exist
if (store.GetProductById(1) == null && store.GetProductById(2) == null)
{
    store.AddProduct(new TeddyBear(1, "Nalle Puh Teddy", 5, 199.99, "Scandic Toys AB", "3+", "Plush"));
    store.AddProduct(new Lego(2, "Lego Star Wars 500pcs", 15, 599, "LEGO Supplier", "5+", 500));
    store.AddProduct(new PlaystationGame(3, "FIFA 26 PS5", 8, 699, "Elgiganten AB", "10+", "PS5"));
    store.AddProduct(new Tillbehor(4, "PS5 Controller Pink", 3, 799, "Webhallen", "10+", "Controller"));
    store.AddProduct(new Candy(5, "Lösgodis Mix Sour", 20, 15.99, "Cloetta AB", "Sour Strawberry", 1000, DateTime.Now.AddDays(25)));
}

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(@"
  ************************************************************
    *              SWEET  AND  PLAY                             *
    *             Toys & Candy Store Management                 *
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
    Console.WriteLine("  6. Search by ID"); // Search by ID feature
    Console.WriteLine("  7. Exit");
    Console.WriteLine("  ------------------------------------------------");
    Console.Write("  Choose [1-7]: ");
    string ch = Console.ReadLine() ?? "";

    // View all products with color coding
    if (ch == "4") { store.ViewProducts(); Console.WriteLine("\n  Press ENTER..."); Console.ReadLine(); }
    // Generate report by category
    else if (ch == "5") { store.GenerateReport(); Console.WriteLine("\n  Press ENTER..."); Console.ReadLine(); }
    // Search product by ID using GetProductById
    else if (ch == "6")
    {
        Console.Clear();
        Console.WriteLine("\n--- SEARCH BY ID ---");
        store.ViewProducts();
        Console.Write("\nEnter ID to search: ");
        if (int.TryParse(Console.ReadLine(), out int searchId))
        {
            var found = store.GetProductById(searchId);
            if (found != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  ✅ FOUND:");
                Console.WriteLine($"  ID: {found.ID} | Name: {found.Name}");
                Console.WriteLine($"  Qty: {found.Quantity} | Price: {found.Price} kr");
                Console.WriteLine($"  Supplier: {found.Supplier}");
                // Show candy-specific details if applicable
                if (found is Candy c) Console.WriteLine($"  Flavor: {c.Flavor} | Expiry: {c.ExpiryDate:yyyy-MM-dd}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  ❌ ID {searchId} not found!");
                Console.ResetColor();
            }
        }
        else
        {
            Console.WriteLine("\n  ❌ Invalid ID!");
        }
        Console.WriteLine("\n  Press ENTER..."); Console.ReadLine();
    }
    // Save and exit
    else if (ch == "7") { store.SaveToFile(); break; }
    // Add new product - multi-level menu
    else if (ch == "1")
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n========== MENU 1: SELECT MAIN TYPE ==========");
        Console.WriteLine(" [1] Godis (Candy) - leaf, no children");
        Console.WriteLine(" [2] Leksaker (Toys) - has 4 sub-types");
        Console.WriteLine($"  Next available ID: {store.GetNextAvailableId()}"); // Suggests next free ID
        Console.WriteLine("===========================================");
        Console.ResetColor();
        Console.Write("Choose [1-2]: ");
        string menu1 = Console.ReadLine() ?? "1";
        try
        {
            Console.Write("\nID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID!"); Console.ReadLine(); continue; }

            // Prevent duplicate ID - check before asking other fields
            if (store.GetProductById(id) != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ ID {id} already exists! Use ID {store.GetNextAvailableId()}");
                Console.ResetColor();
                Console.WriteLine("\n  Press ENTER..."); Console.ReadLine();
                continue;
            }

            Console.Write("Name: "); string name = Console.ReadLine() ?? "";
            Console.Write("Quantity: "); int qty = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Price (kr): "); double price = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Supplier: "); string sup = Console.ReadLine() ?? "Unknown";

            bool added = false;
            // Candy branch - leaf node
            if (menu1 == "1")
            {
                Console.Write("Flavor: "); string flav = Console.ReadLine() ?? "Sweet";
                Console.Write("Weight (1000): "); int weight = int.TryParse(Console.ReadLine(), out int w) ? w : 1000;
                Console.Write("Expiry yyyy-MM-dd (default=30d): "); string expStr = Console.ReadLine() ?? "";
                DateTime exp = string.IsNullOrWhiteSpace(expStr) ? DateTime.Now.AddDays(30) : DateTime.Parse(expStr);
                added = store.AddProduct(new Candy(id, name, qty, price, sup, flav, weight, exp));
            }
            else
            {
                // Toys branch - second level menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n========== MENU 2: SELECT TOY TYPE ==========");
                Console.WriteLine(" [1] Teddy Bear (3+)\n [2] Lego (5+)\n [3] Digital Games -> (Menu 3)\n [4] Custom Toy (Barbie, HotWheels, etc.)");
                Console.WriteLine("==========================================");
                Console.ResetColor();
                Console.Write("Choose [1-4]: ");
                string menu2 = Console.ReadLine() ?? "1";

                if (menu2 == "1")
                {
                    Console.Write("Material (Plush): "); string mat = Console.ReadLine() ?? "Plush";
                    added = store.AddProduct(new TeddyBear(id, name, qty, price, sup, "3+", mat));
                }
                else if (menu2 == "2")
                {
                    Console.Write("Pieces: "); int pieces = int.TryParse(Console.ReadLine(), out int p) ? p : 500;
                    added = store.AddProduct(new Lego(id, name, qty, price, sup, "5+", pieces));
                }
                else if (menu2 == "4")
                {
                    Console.Write("Which type? (Barbie, HotWheels, Cars): "); string customType = Console.ReadLine() ?? "Barbie";
                    Console.Write("Age (e.g 4+): "); string age = Console.ReadLine() ?? "4+";
                    Console.Write("Description (e.g Doll): "); string desc = Console.ReadLine() ?? "";
                    added = store.AddProduct(new CustomToy(id, name, qty, price, sup, age, customType, desc));
                }
                else
                {
                    // Digital Games - third level menu, has 2 children
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n========== MENU 3: DIGITAL GAMES ==========");
                    Console.WriteLine(" [1] Playstation Game\n [2] Tillbehör (Accessory)");
                    Console.WriteLine("============================================");
                    Console.ResetColor();
                    Console.Write("Choose [1-2]: ");
                    string menu3 = Console.ReadLine() ?? "1";

                    if (menu3 == "1")
                    {
                        Console.Write("Platform (PS5): "); string plat = Console.ReadLine() ?? "PS5";
                        added = store.AddProduct(new PlaystationGame(id, name, qty, price, sup, "10+", plat));
                    }
                    else
                    {
                        Console.Write("Category: "); string cat = Console.ReadLine() ?? "Accessory";
                        added = store.AddProduct(new Tillbehor(id, name, qty, price, sup, "10+", cat));
                    }
                }
            }
            // Show success only if actually added
            if (added)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  ✅ Added and saved! {name}");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ❌ Error: {ex.Message}"); Console.ResetColor();
        }
        Console.WriteLine("\n  Press ENTER..."); Console.ReadLine();
    }
    // Update existing product
    else if (ch == "2")
    {
        Console.Clear();
        Console.WriteLine("\n--- UPDATE PRODUCT ---");
        store.ViewProducts();
        Console.Write("\nEnter ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var existing = store.GetProductById(id);
            if (existing == null) Console.WriteLine("\n  ❌ Not found!");
            else
            {
                Console.WriteLine($"\n  Current: {existing.Name} | {existing.Supplier} | Qty:{existing.Quantity} | {existing.Price}kr");
                Console.WriteLine("  (Leave empty = no change)\n");
                Console.Write("New Name: "); string nNameStr = Console.ReadLine() ?? ""; string? nName = string.IsNullOrWhiteSpace(nNameStr) ? null : nNameStr;
                Console.Write("New Supplier: "); string nSupStr = Console.ReadLine() ?? ""; string? nSup = string.IsNullOrWhiteSpace(nSupStr) ? null : nSupStr;
                Console.Write("New Quantity: "); string qStr = Console.ReadLine() ?? ""; int? newQty = null; if (!string.IsNullOrWhiteSpace(qStr) && int.TryParse(qStr, out int qVal)) newQty = qVal;
                Console.Write("New Price kr: "); string pStr = Console.ReadLine() ?? ""; double? newPrice = null; if (!string.IsNullOrWhiteSpace(pStr) && double.TryParse(pStr, out double pVal)) newPrice = pVal;
                string? nFlavor = null;
                if (existing is Candy) { Console.Write("New Flavor (Candy): "); string fStr = Console.ReadLine() ?? ""; if (!string.IsNullOrWhiteSpace(fStr)) nFlavor = fStr; }
                bool ok = store.UpdateProduct(id, newQty, newPrice, nName, nSup, nFlavor);
                if (ok) { Console.ForegroundColor = ConsoleColor.Green; var updated = store.GetProductById(id); Console.WriteLine($"\n  ✅ Updated and saved! {updated?.Name} | {updated?.Supplier}"); Console.ResetColor(); }
                else Console.WriteLine("\n  ❌ Error during update!");
            }
        }
        Console.WriteLine("\n  Press ENTER..."); Console.ReadLine();
    }
    // Delete product by ID
    else if (ch == "3")
    {
        Console.Clear();
        Console.WriteLine("\n--- DELETE PRODUCT ---");
        store.ViewProducts();
        Console.Write("\nEnter ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write($"Are you sure to delete ID {id}? (y/n): ");
            string conf = Console.ReadLine() ?? "n";
            if (conf.ToLower() == "j" || conf.ToLower() == "ja" || conf.ToLower() == "y")
            {
                bool ok = store.DeleteProduct(id);
                Console.WriteLine(ok ? $"\n  ✅ Deleted and saved! ID {id}" : "\n  ❌ Not found!");
            }
        }
        Console.WriteLine("\n  Press ENTER..."); Console.ReadLine();
    }
}