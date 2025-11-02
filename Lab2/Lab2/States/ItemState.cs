using RPGInventorySystem.Items;
using RPGInventorySystem.Modules;

namespace RPGInventorySystem.States
{
    public interface IItemState
    {
        void HandleDamage(Armor armor, int amount);
        
        void Equip(Armor armor, Player player);

        void Unequip(Armor armor, Player player);
    }
}