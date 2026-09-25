namespace LAB1
{
    public interface IPersonRepository
    {
        IReadOnlyCollection<Person> GetAll();
        Person FindByName(string name);
        void Add(Person person);
        int RemoveByName(string name);
        IEnumerable<Person> GetOlderThan(int minAge);
    }
}