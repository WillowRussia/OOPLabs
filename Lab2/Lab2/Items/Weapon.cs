using RPGInventorySystem.Modules;

namespace RPGInventorySystem.Items
{
    public class Weapon : IEquippable
    {
        public string Name { get; }
        public string Description => $"Оружие с уроном {Damage}. [{Rarity}]";
        public int Damage { get; }
        public ItemRarity Rarity { get; }

        public Weapon(string name, int damage, ItemRarity rarity = ItemRarity.Common)
        {
            Name = name;
            Damage = damage;
            Rarity = rarity;
        }

        public void Equip(Player player) => player.AddAttack(Damage);
        public void Unequip(Player player) => player.AddAttack(-Damage);
        public void Accept(IItemVisitor visitor) => visitor.Visit(this);
    }
}