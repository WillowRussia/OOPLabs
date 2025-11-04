using RPGInventorySystem.Modules;
using RPGInventorySystem.Strategies;

namespace RPGInventorySystem.Items
{
    public class Potion : IUsable
    {
        public string Name { get; }
        public string Description { get; }
        public ItemRarity Rarity { get; }
        private readonly IUseStrategy _useStrategy;

        public Potion(string name, string description, IUseStrategy useStrategy, ItemRarity rarity = ItemRarity.Common)
        {
            Name = name;
            Description = description;
            _useStrategy = useStrategy;
            Rarity = rarity;
        }

        public void Use(Player player) => _useStrategy.Use(player, this);
        public void Accept(IItemVisitor visitor) => visitor.Visit(this);
    }
}