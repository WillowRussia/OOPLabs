namespace RPGInventorySystem.Modules
{
    public interface IUsable : IItem
    {
        void Use(Player player);
    }
}