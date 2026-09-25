public abstract class Employer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public decimal Salary { get; set; }
    public int WorkExperienceYears { get; set; }
    public bool HasHigherEducation { get; set; }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {FirstName} {LastName} | Вік: {Age} | Стаж: {WorkExperienceYears} р. | Оклад: {Salary} грн.";
    }
}

public class President : Employer { }
public class Manager : Employer { }
public class Worker : Employer { }

public class Company_Task3
{
    public string Name { get; set; }
    public List<Employer> Employees { get; set; } = new List<Employer>();
}