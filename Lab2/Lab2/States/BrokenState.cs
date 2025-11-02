using RPGInventorySystem.Items;
using RPGInventorySystem.Modules;

namespace RPGInventorySystem.States
{

    public class BrokenState : IItemState
    {
        public void HandleDamage(Armor armor, int amount)
        {
            Console.WriteLine($"{armor.Name} уже сломан и не может получить больше урона.");
        }

        public void Equip(Armor armor, Player player)
        {
            Console.WriteLine($"Невозможно экипировать {armor.Name}, так как он сломан.");
        }

        public void Unequip(Armor armor, Player player)
        {
            Console.WriteLine($"{armor.Name} уже сломан и не давал бонусов.");
        }
    }
}