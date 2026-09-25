using System;

namespace GameShop
{
    public class InGameStoreService : IGameShopService
    {
        private Item[] _items = new Item[2]; 
        private int _count = 0;              

        public void AddItem(Item item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i].Id == item.Id)
                {
                    Console.WriteLine($"Помилка: Предмет з ID {item.Id} вже існує!");
                    return;
                }
            }

            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }

            _items[_count] = item;
            _count++;
            Console.WriteLine($"Успішно додано: {item.Name}");
        }

        public bool RemoveItem(int itemId)
        {
            int targetIndex = -1;

            for (int i = 0; i < _count; i++)
            {
                if (_items[i].Id == itemId)
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex == -1)
            {
                Console.WriteLine($"Помилка: Предмет з ID {itemId} не знайдено!");
                return false;
            }

            Console.WriteLine($"Успішно видалено: {_items[targetIndex].Name}");

            for (int i = targetIndex; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            _count--;
            _items[_count] = default; 
            return true;
        }

        public bool UpdateItem(int itemId, int newQuantity, double newPrice)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i].Id == itemId)
                {
                    _items[i].Quantity = Math.Max(0, newQuantity);
                    _items[i].Price = Math.Max(0.0, newPrice);
                    Console.WriteLine($"Характеристики предмета '{_items[i].Name}' оновлено.");
                    return true;
                }
            }

            Console.WriteLine($"Помилка: Предмет з ID {itemId} не знайдено для оновлення!");
            return false;
        }

        public Item[] GetAllItems()
        {
            Item[] result = new Item[_count];
            Array.Copy(_items, result, _count);
            return result;
        }
    }
}