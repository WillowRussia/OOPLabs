using RPGInventorySystem.Items;

namespace RPGInventorySystem.Factories
{
    public class BasicItemFactory : IItemFactory
    {
        public Weapon CreateWeapon() => new Weapon("Простой меч", 10);
        public Armor CreateArmor() => new Armor("Кожаная броня", 5);
    }
}