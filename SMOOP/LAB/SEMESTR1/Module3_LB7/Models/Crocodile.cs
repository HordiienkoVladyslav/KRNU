namespace Module3_LB7
{
    public class Crocodile : Animal
    {
        public Crocodile(string name, double food, bool breed, bool social)
            : base(name, food, "Риба/М'ясо", breed, social)
        {
            Species = "Крокодил";
        }

        public override string GetHousingRequirements() =>
            "Тераріум з підігрівом води та береговою зоною.";
    }
}