using RPGInventorySystem.Items;

namespace RPGInventorySystem.Factories
{
    public interface IItemFactory
    {
        Weapon CreateWeapon();
        Armor CreateArmor();
    }
}