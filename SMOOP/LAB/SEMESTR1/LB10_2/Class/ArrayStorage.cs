using System;
using System.Collections.Generic;
using System.Text;


namespace LB10_2;

public class ArrayStorage : IStorage
{
    private readonly Item[] _items;
    public int Count { get; private set; }

    public ArrayStorage(int capacity)
    {
        _items = new Item[capacity];
        Count = 0;
    }

    public void Add(Item item)
    {
        if (Count >= _items.Length)
        {
            throw new Exception("Сховище валізи фізично переповнене (немає місця в масиві)!");
        }
        _items[Count] = item;
        Count++;
    }

    public double GetTotalVolume()
    {
        double total = 0;
        for (int i = 0; i < Count; i++)
        {
            total += _items[i].Volume;
        }
        return total;
    }
}
