namespace Module3_LB7
{
    public class ZooService
    {
        private Enclosure[] _enclosures = System.Array.Empty<Enclosure>();

        public Enclosure[] GetEnclosures() => _enclosures;

        public void AddEnclosure(string type, int capacity)
        {
            ArrayHelper.AddElement(ref _enclosures, new Enclosure(type, capacity));
        }

        public bool TryAccodomateAnimal(IAnimal animal, int enclosureIndex, out string message)
        {
            if (enclosureIndex < 0 || enclosureIndex >= _enclosures.Length)
            {
                message = "Помилка: Невірно обраний вольєр.";
                return false;
            }

            Enclosure target = _enclosures[enclosureIndex];

            if (target.CanAccept(animal))
            {
                target.AddAnimal(animal);
                message = $"УСПІХ: {animal.Name} оселено у вольєр '{target.Type}'.";
                return true;
            }

            message = $"ВІДМОВА: Вольєр '{target.Type}' не підходить для {animal.Name} (через сумісність або брак місця).";
            return false;
        }

        public double CalculateMonthlyFoodTotal()
        {
            double dailySum = 0;
            foreach (var enc in _enclosures)
            {
                for (int i = 0; i < enc.CurrentCount; i++)
                {
                    dailySum += enc.AnimalsIn[i].DailyFoodAmount;
                }
            }
            return dailySum * 30;
        }
    }
}