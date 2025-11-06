using RPGInventorySystem.Modules;
using RPGInventorySystem.Items;
using RPGInventorySystem.Factories;
using RPGInventorySystem.Builders;
using RPGInventorySystem.Strategies;

namespace RPGInventorySystem.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём игрока и фабрики
            var player = new Player();
            var basicFactory = new BasicItemFactory();
            var magicalFactory = new MagicalItemFactory();

            // Создаём оружие через Builder
            var builder = new WeaponBuilder()
                .WithName("Custom Blade")
                .WithDamage(12)
                .WithRarity(ItemRarity.Uncommon);
            var customWeapon = builder.Build();

            // Добавляем предметы в инвентарь
            player.Inventory.AddItem(customWeapon);
            var basicArmor = basicFactory.CreateArmor();
            player.Inventory.AddItem(basicArmor);

            var potion = new Potion("Small Heal", "Restores 20 HP", new HealStrategy(20), ItemRarity.Common);
            player.Inventory.AddItem(potion);

            Console.WriteLine("Начальное состояние инвентаря:");
            Console.WriteLine(player.Inventory.DisplayInventory());
            Console.WriteLine();

            // Экипируем оружие
            Console.WriteLine($"Экипируем {customWeapon.Name}...");
            customWeapon.Equip(player);
            Console.WriteLine($"Атака игрока: {player.Attack}");
            Console.WriteLine();

            // Используем зелье
            player.Health = 60;
            Console.WriteLine($"Здоровье игрока до употребления зелья: {player.Health}");
            Console.WriteLine($"Используем {potion.Name}...");
            potion.Use(player);
            Console.WriteLine($"Здоровье игрока после употребления: {player.Health}");
            Console.WriteLine("Инвентарь после использования зелья:");
            Console.WriteLine(player.Inventory.DisplayInventory());
            Console.WriteLine();

            // Улучшаем оружие через сервис улучшений
            var upgrader = new ItemUpgradeService();
            Console.WriteLine($"Улучшаем оружие {customWeapon.Name}...");
            var upgradedWeapon = upgrader.UpgradeWeapon(customWeapon, extraDamage: 8, newRarity: ItemRarity.Rare);

            // Если оружие было экипировано — снимаем старое и экипируем новое
            customWeapon.Unequip(player);
            player.Inventory.RemoveItem(customWeapon);
            player.Inventory.AddItem(upgradedWeapon);
            upgradedWeapon.Equip(player);

            Console.WriteLine($"Новое оружие: {upgradedWeapon.Name} ({upgradedWeapon.Rarity}), урон {upgradedWeapon.Damage}");
            Console.WriteLine($"Атака игрока после апгрейда: {player.Attack}");
            Console.WriteLine();
            Console.WriteLine("Итоговый инвентарь:");
            Console.WriteLine(player.Inventory.DisplayInventory());

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}