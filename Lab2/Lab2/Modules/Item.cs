namespace RPGInventorySystem.Modules
{
    public enum ItemRarity
    {
        Common, Uncommon, Rare, Epic, Legendary
    }

    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        ItemRarity Rarity { get; }
        void Accept(IItemVisitor visitor);
    }
    
    public interface IItemVisitor
    {
        void Visit(IEquippable equippable);
        void Visit(IUsable usable);
        void Visit(IItem item);
    }
}