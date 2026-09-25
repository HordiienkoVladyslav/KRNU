using LB7_Zoo;

public class Tiger : Animal
{
    public Tiger(string name, double food, bool breed, bool social)
        : base(name, food, "Яловичина", breed, social)
    {
        Species = "Тигр";
    }

    public override string GetHousingRequirements() => "Потрібен вольєр з посиленими гратами та зоною для полювання.";
}