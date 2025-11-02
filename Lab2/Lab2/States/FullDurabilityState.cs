using RPGInventorySystem.Items;
using RPGInventorySystem.Modules;

namespace RPGInventorySystem.States
{
    public class FullDurabilityState : IItemState
    {
        public void HandleDamage(Armor armor, int amount)
        {
            Console.WriteLine($"Прочность {armor.Name} снизилась. Состояние изменено на 'Повреждено'.");
            armor.State = new DamagedState();
        }

        public void Equip(Armor armor, Player player)
        {
            Console.WriteLine($"{armor.Name} экипирован. +{armor.Defense} к защите.");
            player.AddDefense(armor.Defense);
        }

        public void Unequip(Armor armor, Player player)
        {
            Console.WriteLine($"{armor.Name} снят.");
            player.AddDefense(-armor.Defense);
        }
    }
}