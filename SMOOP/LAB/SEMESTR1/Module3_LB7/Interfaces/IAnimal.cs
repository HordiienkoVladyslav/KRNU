namespace Module3_LB7
{
    public interface IAnimal
    {
        string Name { get; }
        string Species { get; }
        double DailyFoodAmount { get; }
        string FoodType { get; }
        bool CanBreedInCaptivity { get; }
        bool IsSocial { get; }

        string GetHousingRequirements();
    }
}