namespace Module3_LB7
{
    public class Kangaroo : Animal
    {
        public Kangaroo(string name, double food, bool breed, bool social)
            : base(name, food, "Трава/Овочі", breed, social)
        {
            Species = "Кенгуру";
        }

        public override string GetHousingRequirements() =>
            "Відкрите поле з навісом, можливість проживання групами.";
    }
}