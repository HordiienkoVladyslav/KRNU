using System;
using System.Collections.Generic;
using System.Text;

namespace LB10_2;

public class Item
{
    public string Name { get; set; }
    public double Volume { get; set; }

    public Item(string name, double volume)
    {
        Name = name;
        Volume = volume;
    }
}
