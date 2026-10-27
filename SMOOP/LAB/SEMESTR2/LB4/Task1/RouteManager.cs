using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

public static class RouteManager
{
    private const string JsonFile = "routes.json";
    private const string XmlFile = "routes.xml";

    public static async Task RunTaskAsync()
    {
        Console.Clear();
        Console.WriteLine("=== ЗАВДАННЯ 1: КЛАС «МАРШРУТ» ТА СЕРІАЛІЗАЦІЯ ===");

        Route[] routes = new Route[]
        {
            new Route("Київ", "Львів", 6.5, 101),
            new Route("Київ", "Одеса", 7.0, 105),
            new Route("Харків", "Київ", 5.5, 202),
            new Route("Київ", "Львів", 5.0, 110),
            new Route("Дніпро", "Одеса", 9.0, 303)
        };

        // --- JSON Серіалізація ---
        string jsonString = JsonSerializer.Serialize(routes, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(JsonFile, jsonString);
        Console.WriteLine($"[JSON] Дані успішно серіалізовано у файл {JsonFile}");

        string readJson = await File.ReadAllTextAsync(JsonFile);
        Route[]? deserializedJsonRoutes = JsonSerializer.Deserialize<Route[]>(readJson);
        Console.WriteLine($"[JSON] Десеріалізовано об'єктів: {deserializedJsonRoutes?.Length ?? 0}");

        // --- XML Серіалізація ---
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Route[]));
        using (FileStream fs = new FileStream(XmlFile, FileMode.Create))
        {
            xmlSerializer.Serialize(fs, routes);
        }
        Console.WriteLine($"[XML] Дані успішно серіалізовано у файл {XmlFile}");

        Route[]? deserializedXmlRoutes = null;
        using (FileStream fs = new FileStream(XmlFile, FileMode.Open))
        {
            deserializedXmlRoutes = xmlSerializer.Deserialize(fs) as Route[];
        }
        Console.WriteLine($"[XML] Десеріалізовано об'єктів: {deserializedXmlRoutes?.Length ?? 0}");

        Console.WriteLine("\n--- Пошук маршрутів за індивідуальним завданням ---");
        Console.Write("Введіть назву пункту призначення (наприклад, Львів або Одеса): ");
        string targetDestination = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Введіть максимальний час подорожі (в годинах): ");
        if (double.TryParse(Console.ReadLine(), out double maxTime))
        {
            var filteredRoutes = Array.FindAll(routes, r =>
                r.EndPoint.Equals(targetDestination, StringComparison.OrdinalIgnoreCase) &&
                r.TravelTime <= maxTime);

            Console.WriteLine($"\nРезультати пошуку до пункту '{targetDestination}' за час до {maxTime} год:");
            if (filteredRoutes.Length > 0)
            {
                foreach (var route in filteredRoutes)
                {
                    Console.WriteLine($" - {route}");
                }
            }
            else
            {
                Console.WriteLine("Маршрутів, що відповідають умовам, не знайдено.");
            }
        }
        else
        {
            Console.WriteLine("Некоректний формат часу.");
        }

        Console.WriteLine("\nНатисніть Enter, щоб повернутися до головного меню...");
        Console.ReadLine();
    }
}