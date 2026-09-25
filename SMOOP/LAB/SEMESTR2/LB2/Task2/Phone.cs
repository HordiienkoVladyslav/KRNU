using System;
using System.Collections.Generic;
using System.Linq;

public class Phone
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    public override string ToString()
    {
        return $"{Manufacturer} {Name} | Ціна: {Price}$ | Реліз: {ReleaseDate:dd.MM.yyyy}";
    }
}