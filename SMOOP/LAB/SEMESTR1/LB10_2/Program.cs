using System;

namespace LB10_2;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо компоненти сховища та валідації
        IStorage storage = new ArrayStorage(10); 
        IPackingValidator validator = new VolumeValidator();
        SuitcasePacker packer = new SuitcasePacker(validator);


        Suitcase mySuitcase = new Suitcase("Жовтий", "Samsonite", 3.2, 40.0, storage);
        Console.WriteLine($"Валіза {mySuitcase.Manufacturer} готова до пакування. Макс. об'єм: {mySuitcase.MaxVolume}л\n");

        // Налаштовуємо СЛУХАЧА ПОДІЇ
        packer.ItemAdded += (sender, item) =>
        {
            Console.WriteLine($"[СЛУХАЧ ПОДІЇ] Успішно додано: {item.Name} (Об'єм: {item.Volume}л)");
            Console.WriteLine($"Поточний стан валізи: {mySuitcase.GetCurrentVolume()}/{mySuitcase.MaxVolume}л\n");
        };


        try
        {
            packer.Pack(mySuitcase, new Item("Джинси", 4.5));
            packer.Pack(mySuitcase, new Item("Куртка", 12.0));
            packer.Pack(mySuitcase, new Item("Ноутбук", 6.0));
            packer.Pack(mySuitcase, new Item("Аптечка", 3.0));

            Console.WriteLine("--- Спроба спакувати великий намет (20л) ---");
            packer.Pack(mySuitcase, new Item("Намет туристичний", 20.0));
        }
        catch (SuitcaseOverflowException ex)
        {

            Console.WriteLine($"[ВИНЯТОК ПЕРЕПОВНЕННЯ] {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ЗАГАЛЬНА ПОМИЛКА] {ex.Message}");
        }
    }
}