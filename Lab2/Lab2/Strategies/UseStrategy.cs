using RPGInventorySystem.Items;
using RPGInventorySystem.Modules;

namespace RPGInventorySystem.Strategies
{
    public interface IUseStrategy
    {
        void Use(Player player, Potion potion);
    }
}