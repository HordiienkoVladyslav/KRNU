using System;

namespace GameShop
{
    public struct Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public ItemCategory Category { get; set; }

        public Item(int id, string name, int quantity, double price, ItemCategory category)
        {
            Id = id;
            Name = name;
            Quantity = Math.Max(0, quantity);
            Price = Math.Max(0.0, price);
            Category = category;
        }

        public override string ToString() =>
            $"[ID: {Id}] {Name} | Категорія: {Category} | Кількість: {Quantity} | Ціна: {Price} gold";
    }
}