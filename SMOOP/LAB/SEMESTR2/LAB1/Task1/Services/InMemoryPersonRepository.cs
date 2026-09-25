namespace LAB1
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly List<Person> _people;

        public InMemoryPersonRepository(IEnumerable<Person> initialData = null)
        {
            _people = initialData?.ToList() ?? new List<Person>();
        }

        public IReadOnlyCollection<Person> GetAll() => _people.AsReadOnly();

        public Person FindByName(string name)
        {
            return _people.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            _people.Add(person);
        }

        public int RemoveByName(string name)
        {
            return _people.RemoveAll(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Person> GetOlderThan(int minAge)
        {
            return _people.Where(p => p.Age > minAge)
                          .OrderBy(p => p.Name);
        }
    }
}