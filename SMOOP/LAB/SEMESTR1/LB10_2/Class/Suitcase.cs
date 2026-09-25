using System;
using System.Collections.Generic;
using System.Text;

namespace LB10_2;

public class Suitcase
{
    public string Color { get; set; }
    public string Manufacturer { get; set; }
    public double Weight { get; set; }
    public double MaxVolume { get; private set; }

    private readonly IStorage _storage;

    public Suitcase(string color, string manufacturer, double weight, double maxVolume, IStorage storage)
    {
        Color = color;
        Manufacturer = manufacturer;
        Weight = weight;
        MaxVolume = maxVolume;
        _storage = storage;
    }

    public void AddToStorage(Item item)
    {
        _storage.Add(item);
    }

    public double GetCurrentVolume()
    {
        return _storage.GetTotalVolume();
    }
}
