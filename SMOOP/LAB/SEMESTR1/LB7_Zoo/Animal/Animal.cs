namespace LB7_Zoo
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public string Species { get; protected set; }
        public double DailyFoodAmount { get; set; }
        public string FoodType { get; set; }
        public bool CanBreedInCaptivity { get; set; }
        public bool IsSocial { get; set; }

        protected Animal(string name, double foodAmount, string foodType, bool canBreed, bool isSocial)
        {
            Name = name;
            DailyFoodAmount = foodAmount;
            FoodType = foodType;
            CanBreedInCaptivity = canBreed;
            IsSocial = isSocial;
        }

        public abstract string GetHousingRequirements();

        public override string ToString() => $"[{Species}] {Name} | Їжа: {FoodType} ({DailyFoodAmount}кг) | Соціальний: {(IsSocial ? "Так" : "Ні")}";

        public override bool Equals(object obj)
        {
            if (obj is Animal other) return Name == other.Name && Species == other.Species;
            return false;
        }

        public override int GetHashCode() => (Name + Species).GetHashCode();
    }
}