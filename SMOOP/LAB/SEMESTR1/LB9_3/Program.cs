using System;

namespace GameShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IGameShopService armoryStore = new InGameStoreService();

            Console.WriteLine("=== ЗАПОВНЕННЯ МАГАЗИНУ ===");
            armoryStore.AddItem(new Item(1, "Frostmourne", 1, 9999, ItemCategory.Weapon));
            armoryStore.AddItem(new Item(2, "Health Potion", 50, 15, ItemCategory.Consumable));
            armoryStore.AddItem(new Item(3, "Paladin Shield", 3, 450, ItemCategory.Armor));

            PrintStoreCatalog(armoryStore);

            Console.WriteLine("\n=== ОНОВЛЕННЯ ТОВАРУ ===");
            armoryStore.UpdateItem(2, 120, 10); 

            PrintStoreCatalog(armoryStore);

            Console.WriteLine("\n=== ВИДАЛЕННЯ ТОВАРУ ===");
            armoryStore.RemoveItem(1);

            PrintStoreCatalog(armoryStore);

            Console.ReadLine();
        }

        static void PrintStoreCatalog(IGameShopService store)
        {
            Console.WriteLine("\n---------------- Вітрина магазину ----------------");
            Item[] catalog = store.GetAllItems(); 

            if (catalog.Length == 0)
            {
                Console.WriteLine("Магазин порожній.");
            }
            else
            {
                foreach (Item item in catalog)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("--------------------------------------------------");
        }
    }
}