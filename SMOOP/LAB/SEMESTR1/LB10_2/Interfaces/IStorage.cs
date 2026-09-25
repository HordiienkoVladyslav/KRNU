using System;
using System.Collections.Generic;
using System.Text;

namespace LB10_2;

public interface IStorage
{
    void Add(Item item);
    double GetTotalVolume();
    int Count { get; }
}
