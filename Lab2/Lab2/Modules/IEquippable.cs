namespace RPGInventorySystem.Modules
{
    public interface IEquippable : IItem
    {
        void Equip(Player player);
        void Unequip(Player player);
    }
}