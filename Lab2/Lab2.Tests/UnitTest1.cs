using RPGInventorySystem.Modules;
using RPGInventorySystem.Items;
using RPGInventorySystem.Builders;
using RPGInventorySystem.Strategies;
using RPGInventorySystem.Factories;
using RPGInventorySystem.States;

public class InventorySystemTests
{
    [Fact]
    public void AddItem_ToInventory_IncreasesItemCount()
    {
        var player = new Player();
        var sword = new Weapon("Тестовый меч", 10);
        player.Inventory.AddItem(sword);
        Assert.Single(player.Inventory.Items);
        Assert.Contains(sword, player.Inventory.Items);
    }

    [Fact]
    public void EquipAndUnequipWeapon_CorrectlyChangesPlayerAttack()
    {
        var player = new Player();
        var initialAttack = player.Attack;
        var weapon = new Weapon("Большой меч", 15);
        weapon.Equip(player);
        Assert.Equal(initialAttack + 15, player.Attack);
        weapon.Unequip(player);
        Assert.Equal(initialAttack, player.Attack);
    }

    [Fact]
    public void UseHealingPotion_RestoresHealthAndIsConsumed()
    {
        var player = new Player { Health = 50 };
        var potion = new Potion("Зелье лечения", "Лечит 25 HP", new HealStrategy(25));
        player.Inventory.AddItem(potion);
        potion.Use(player);
        Assert.Equal(75, player.Health);
        Assert.DoesNotContain(potion, player.Inventory.Items);
    }

    [Fact]
    public void WeaponBuilder_CreatesWeaponWithCorrectAttributes()
    {
        var builder = new WeaponBuilder();
        var legendarySword = builder
            .WithName("Экскалибур")
            .WithDamage(100)
            .WithRarity(ItemRarity.Legendary)
            .Build();
        Assert.Equal("Экскалибур", legendarySword.Name);
        Assert.Equal(100, legendarySword.Damage);
        Assert.Equal(ItemRarity.Legendary, legendarySword.Rarity);
    }

    [Fact]
    public void MagicalFactory_CreatesStrongerItemsThanBasicFactory()
    {
        var basicFactory = new BasicItemFactory();
        var magicalFactory = new MagicalItemFactory();
        var basicWeapon = basicFactory.CreateWeapon();
        var magicalWeapon = magicalFactory.CreateWeapon();
        Assert.True(magicalWeapon.Damage > basicWeapon.Damage, "Магическое оружие должно быть сильнее.");
        Assert.True(magicalWeapon.Rarity > basicWeapon.Rarity, "Магическое оружие должно иметь большую редкость.");
    }

    [Fact]
    public void BrokenArmor_ProvidesNoDefenseBonus()
    {
        var player = new Player();
        var initialDefense = player.Defense;
        var armor = new Armor("Сломанная кираса", 20);
        armor.State = new BrokenState();
        armor.Equip(player);
        Assert.Equal(initialDefense, player.Defense);
    }

    [Fact]
    public void DamagedArmor_ProvidesReducedDefenseBonus()
    {
        var player = new Player();
        var initialDefense = player.Defense;
        var armor = new Armor("Поврежденная палка", 20);
        armor.State = new DamagedState();
        armor.Equip(player);
        int expectedDefense = initialDefense + (int)(armor.Defense * 0.5);
        Assert.Equal(expectedDefense, player.Defense);
    }
    
    [Fact]
    public void ItemUpgradeService_CreatesUpgradedWeapon()
    {
        var upgrader = new ItemUpgradeService();
        var basicSword = new Weapon("Железный меч", 10, ItemRarity.Common);
        var upgradedSword = upgrader.UpgradeWeapon(basicSword, extraDamage: 5, newRarity: ItemRarity.Uncommon);
        Assert.Equal(basicSword.Damage + 5, upgradedSword.Damage);
        Assert.Equal(ItemRarity.Uncommon, upgradedSword.Rarity);
        Assert.NotEqual(basicSword, upgradedSword);
    }
}