using System.IO;
using System.Xml.Serialization;
using StudentsApp.Models;

namespace StudentsApp.Services;

/// <summary>Завантаження та збереження списку студентів у XML-файл.</summary>
public class StudentRepository
{
    private readonly string _filePath;

    public StudentRepository(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "Students.xml");
    }

    public List<Student> Load()
    {
        if (!File.Exists(_filePath))
            return CreateDefaultList();

        try
        {
            using var stream = File.OpenRead(_filePath);
            var serializer = new XmlSerializer(typeof(List<Student>));
            return serializer.Deserialize(stream) as List<Student> ?? new List<Student>();
        }
        catch (Exception ex) when (ex is InvalidOperationException or IOException or UnauthorizedAccessException)
        {
            // Пошкоджений файл — стартуємо з початкового списку.
            return CreateDefaultList();
        }
    }

    public void Save(IEnumerable<Student> students)
    {
        using var stream = File.Create(_filePath);
        var serializer = new XmlSerializer(typeof(List<Student>));
        serializer.Serialize(stream, students.ToList());
    }

    private static List<Student> CreateDefaultList() => new()
    {
        new Student { LastName = "Шевченко", FirstName = "Олена", Age = 19, Gender = "Жіноча" },
        new Student { LastName = "Коваль", FirstName = "Андрій", Age = 21, Gender = "Чоловіча" },
        new Student { LastName = "Бондаренко", FirstName = "Марія", Age = 18, Gender = "Жіноча" },
    };
}
