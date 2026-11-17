using System.Xml.Serialization;
using StudentsApp.Infrastructure;

namespace StudentsApp.Models;

/// <summary>Модель даних: студент. Публічний конструктор без параметрів потрібен для XmlSerializer.</summary>
public class Student : ObservableObject
{
    private string _lastName = string.Empty;
    private string _firstName = string.Empty;
    private int _age;
    private string _gender = string.Empty;
    private bool _isSelected;

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }

    public string Gender
    {
        get => _gender;
        set => SetProperty(ref _gender, value);
    }

    /// <summary>
    /// Стан вибору в списку. Прив'язується до ListBoxItem.IsSelected, щоб ViewModel
    /// знала про всі вибрані елементи без Code-Behind. У XML не зберігається.
    /// </summary>
    [XmlIgnore]
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    /// <summary>Порівняння за змістом: прізвище, ім'я, вік і стать (без урахування регістру та пробілів по краях).</summary>
    public static bool AreSame(string lastName1, string firstName1, int age1, string? gender1,
                               string lastName2, string firstName2, int age2, string? gender2)
        => age1 == age2
           && Eq(lastName1, lastName2)
           && Eq(firstName1, firstName2)
           && Eq(gender1, gender2);

    public bool IsSameAs(string lastName, string firstName, int age, string? gender)
        => AreSame(LastName, FirstName, Age, Gender, lastName, firstName, age, gender);

    private static bool Eq(string? a, string? b)
        => string.Equals((a ?? string.Empty).Trim(), (b ?? string.Empty).Trim(), StringComparison.OrdinalIgnoreCase);
}
