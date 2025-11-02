using RPGInventorySystem.Items;
using RPGInventorySystem.Modules;

namespace RPGInventorySystem.States
{

    public class DamagedState : IItemState
    {
        private const double DefenseModifier = 0.5;

        public void HandleDamage(Armor armor, int amount)
        {
            Console.WriteLine($"{armor.Name} полностью сломан!");
            armor.State = new BrokenState();
        }

        public void Equip(Armor armor, Player player)
        {
            int effectiveDefense = (int)(armor.Defense * DefenseModifier);
            Console.WriteLine($"{armor.Name} (повреждено) экипирован. +{effectiveDefense} к защите (эффективность снижена).");
            player.AddDefense(effectiveDefense);
        }

        public void Unequip(Armor armor, Player player)
        {
            int effectiveDefense = (int)(armor.Defense * DefenseModifier);
            Console.WriteLine($"{armor.Name} (повреждено) снят.");
            player.AddDefense(-effectiveDefense);
        }
    }
}