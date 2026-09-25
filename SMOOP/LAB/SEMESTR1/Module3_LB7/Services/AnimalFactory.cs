namespace Module3_LB7
{
    public static class AnimalFactory
    {
        public static IAnimal CreateAnimal(string typeId, string name, double food, bool breed, bool social)
        {
            return typeId switch
            {
                "1" => new Tiger(name, food, breed, social),
                "2" => new Crocodile(name, food, breed, social),
                "3" => new Kangaroo(name, food, breed, social),
                _ => null
            };
        }
    }
}