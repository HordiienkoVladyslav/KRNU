using System;
using System.Collections.Generic;
using System.Linq;

// Базовий клас для всіх співробітників
public abstract class Employer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public double Salary { get; set; }
    public int WorkExperienceYears { get; set; } // Стаж роботи у роках
    public bool HasHigherEducation { get; set; }  // Наявність вищої освіти

    public int Age => DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

    protected Employer(string firstName, string lastName, DateTime birthDate, double salary, int experience, bool higherEducation)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Salary = salary;
        WorkExperienceYears = experience;
        HasHigherEducation = higherEducation;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} | Вік: {Age} | Стаж: {WorkExperienceYears} р. | Оклад: {Salary:C} | Освіта: {(HasHigherEducation ? "Вища" : "Середня/Спеціальна")}";
    }
}

// Похідні класи для конкретних посад
public class President : Employer
{
    public President(string firstName, string lastName, DateTime birthDate, double salary, int experience, bool higherEducation)
        : base(firstName, lastName, birthDate, salary, experience, higherEducation) { }
}

public class Manager : Employer
{
    public Manager(string firstName, string lastName, DateTime birthDate, double salary, int experience, bool higherEducation)
        : base(firstName, lastName, birthDate, salary, experience, higherEducation) { }
}

public class Worker : Employer
{
    public Worker(string firstName, string lastName, DateTime birthDate, double salary, int experience, bool higherEducation)
        : base(firstName, lastName, birthDate, salary, experience, higherEducation) { }
}

// Клас Підприємства
public class Company
{
    public string Name { get; set; }
    public List<Employer> Employees { get; set; } = new List<Employer>();

    public Company(string name)
    {
        Name = name;
    }
}