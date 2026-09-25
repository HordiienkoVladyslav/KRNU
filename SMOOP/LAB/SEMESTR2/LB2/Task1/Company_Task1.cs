using System;
using System.Collections.Generic;
using System.Linq;

public class Company_Task1
{
    public string Name { get; set; }
    public DateTime FoundationDate { get; set; }
    public string BusinessProfile { get; set; }
    public string DirectorFullName { get; set; }
    public int EmployeesCount { get; set; }
    public string Address { get; set; }

    public override string ToString()
    {
        return $"[{Name}] Профіль: {BusinessProfile} | Директор: {DirectorFullName} | " +
               $"Працівників: {EmployeesCount} | Адреса: {Address} | Дата заснування: {FoundationDate:dd.MM.yyyy}";
    }
}