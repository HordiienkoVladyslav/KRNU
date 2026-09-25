using LB7_Zoo;

public class Enclosure
{
    public string Type { get; set; }
    public Animal[] AnimalsIn { get; private set; }
    private int _count = 0;

    public Enclosure(string type, int capacity)
    {
        Type = type;
        AnimalsIn = new Animal[capacity];
    }

    // Метод перевірки: чи можна підселити цю тварину?
    public bool CanAccept(Animal newcomer)
    {
        // 1. Перевірка на вільне місце
        if (_count >= AnimalsIn.Length) return false;

        // 2. Якщо вольєр порожній — можна селити будь-кого
        if (_count == 0) return true;

        // 3. Якщо вже хтось є — перевіряємо сумісність
        Animal existingResidident = AnimalsIn[0];

        // Правило: хижаки (Тигри/Крокодили) живуть тільки зі своїм видом
        // Кенгуру не можуть жити з хижаками
        if (newcomer.Species != existingResidident.Species)
        {
            return false;
        }

        // 4. Перевірка на соціальність (якщо тварина одинак, до неї не можна нікого підселяти)
        if (!newcomer.IsSocial || !existingResidident.IsSocial)
        {
            return false;
        }

        return true;
    }

    public void AddAnimal(Animal animal)
    {
        AnimalsIn[_count++] = animal;
    }

    public int CurrentCount => _count;
}