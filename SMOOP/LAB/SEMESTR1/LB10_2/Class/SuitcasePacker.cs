using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace LB10_2;

public class SuitcasePacker
{
    private readonly IPackingValidator _validator;

    // Подія, яка сигналізує про успішне додавання речі
    public event EventHandler<Item> ItemAdded;

    public SuitcasePacker(IPackingValidator validator)
    {
        _validator = validator;
    }

    public void Pack(Suitcase suitcase, Item item)
    {
        // Перевіряємо правила (чи влізе) за допомогою валідатора
        _validator.Validate(suitcase, item);

        // Якщо все ок — додаємо у валізу
        suitcase.AddToStorage(item);

        // Спрацьовує подія (викликаємо слухача)
        ItemAdded?.Invoke(this, item);
    }
}
