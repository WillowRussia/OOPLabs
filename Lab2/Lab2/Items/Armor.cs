using RPGInventorySystem.Modules;
using RPGInventorySystem.States;

namespace RPGInventorySystem.Items
{
    public class Armor : IEquippable
    {
        public string Name { get; }
        public string Description => $"Броня с защитой {Defense}. Состояние: {State.GetType().Name}. [{Rarity}]";
        public int Defense { get; }
        public ItemRarity Rarity { get; }
        
        public IItemState State { get; set; }

        public Armor(string name, int defense, ItemRarity rarity = ItemRarity.Common)
        {
            Name = name;
            Defense = defense;
            Rarity = rarity;
            State = new FullDurabilityState();
        }

        public void TakeDamage(int amount) => State.HandleDamage(this, amount);
        public void Equip(Player player) => State.Equip(this, player);
        public void Unequip(Player player) => State.Unequip(this, player);
        public void Accept(IItemVisitor visitor) => visitor.Visit(this);
    }
}