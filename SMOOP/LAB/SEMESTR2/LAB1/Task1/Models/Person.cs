namespace LAB1
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public Person(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public override string ToString()
        {
            return $"{Name,-15} | Вік: {Age,3} | Місто: {City}";
        }
    }
}