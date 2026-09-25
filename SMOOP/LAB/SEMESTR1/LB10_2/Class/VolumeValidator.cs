using System;
using System.Collections.Generic;
using System.Text;

namespace LB10_2;

public class VolumeValidator : IPackingValidator
{
    public void Validate(Suitcase suitcase, Item item)
    {
        if (suitcase.GetCurrentVolume() + item.Volume > suitcase.MaxVolume)
        {
            throw new SuitcaseOverflowException(
                $"Неможливо додати '{item.Name}'. Перевищено допустимий об'єм! " +
                $"(Макс: {suitcase.MaxVolume}, Зайнято: {suitcase.GetCurrentVolume()}, Потрібно: {item.Volume})"
            );
        }
    }
}
