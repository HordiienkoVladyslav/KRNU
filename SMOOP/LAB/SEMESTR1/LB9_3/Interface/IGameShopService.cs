namespace GameShop
{
    public interface IGameShopService
    {
        void AddItem(Item item);
        bool RemoveItem(int itemId);
        bool UpdateItem(int itemId, int newQuantity, double newPrice);
        Item[] GetAllItems(); 
    }
}