using System;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    private static readonly HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Координати Кременчука (latitude=49.06, longitude=33.42)
        string url = "https://api.open-meteo.com/v1/forecast?latitude=49.06&longitude=33.42&current=temperature_2m,wind_speed_10m";

        try
        {
            using HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using System.IO.Stream stream = await response.Content.ReadAsStreamAsync();
            using JsonDocument doc = await JsonDocument.ParseAsync(stream);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("current", out JsonElement currentSection))
            {
                double temperature = currentSection.GetProperty("temperature_2m").GetDouble();
                double windSpeed = currentSection.GetProperty("wind_speed_10m").GetDouble();

                string tempUnit = root.GetProperty("current_units").GetProperty("temperature_2m").GetString();
                string windUnit = root.GetProperty("current_units").GetProperty("wind_speed_10m").GetString();

                Console.WriteLine("Дані для м. Кременчук успішно витягнуто!");
                Console.WriteLine($"Температура: {temperature} {tempUnit}");
                Console.WriteLine($"Швидкість вітру: {windSpeed} {windUnit}");
            }
            else
            {
                Console.WriteLine("'current' не знайдено у відповіді JSON");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}