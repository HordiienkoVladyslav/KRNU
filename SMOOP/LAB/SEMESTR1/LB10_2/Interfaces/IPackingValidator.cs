using System;
using System.Collections.Generic;
using System.Text;

namespace LB10_2;

public interface IPackingValidator
{
    void Validate(Suitcase suitcase, Item item);
}
