using RPGInventorySystem.Items;
using RPGInventorySystem.Modules;

namespace RPGInventorySystem.Strategies
{
    public class HealStrategy : IUseStrategy
    {
        private readonly int _healAmount;
        public HealStrategy(int healAmount) => _healAmount = healAmount;

        public void Use(Player player, Potion potion)
        {
            player.Health = Math.Min(100, player.Health + _healAmount);
            player.Inventory.RemoveItem(potion);
            Console.WriteLine($"Игрок использовал {potion.Name} и восстановил {_healAmount} здоровья.");
        }
    }
}