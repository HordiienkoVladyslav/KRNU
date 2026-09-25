namespace Module3_LB7
{
    public class Enclosure
    {
        public string Type { get; set; }
        public IAnimal[] AnimalsIn { get; private set; }
        private int _count = 0;

        public Enclosure(string type, int capacity)
        {
            Type = type;
            AnimalsIn = new IAnimal[capacity];
        }

        public bool CanAccept(IAnimal newcomer)
        {
            if (_count >= AnimalsIn.Length) return false;
            if (_count == 0) return true;

            IAnimal existingResident = AnimalsIn[0];

            // Перевірка сумісності видів та соціальності
            if (newcomer.Species != existingResident.Species) return false;
            if (!newcomer.IsSocial || !existingResident.IsSocial) return false;

            return true;
        }

        public void AddAnimal(IAnimal animal)
        {
            AnimalsIn[_count++] = animal;
        }

        public int CurrentCount => _count;
        public bool IsFull => _count >= AnimalsIn.Length;
    }
}