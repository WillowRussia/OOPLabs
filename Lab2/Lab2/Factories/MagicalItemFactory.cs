using RPGInventorySystem.Modules;
using RPGInventorySystem.Items;

namespace RPGInventorySystem.Factories
{
    public class MagicalItemFactory : IItemFactory
    {
        public Weapon CreateWeapon() => new Weapon("Магический меч", 15, ItemRarity.Uncommon);
        public Armor CreateArmor() => new Armor("Магическая броня", 10, ItemRarity.Rare);
    }
}