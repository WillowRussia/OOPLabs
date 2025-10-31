using System.Text;

namespace RPGInventorySystem.Modules
{
    public class Inventory
    {
        private readonly List<IItem> _items = new List<IItem>();
        public IReadOnlyList<IItem> Items => _items.AsReadOnly();

        public void AddItem(IItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
        }

        public string DisplayInventory()
        {
            if (!_items.Any()) return "Инвентарь пуст.";
            
            var sb = new StringBuilder("Инвентарь:\n");
            foreach (var item in _items)
            {
                sb.AppendLine($"- {item.Name} [{item.Rarity}]");
            }
            return sb.ToString();
        }
    }
}