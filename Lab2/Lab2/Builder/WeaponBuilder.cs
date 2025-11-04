using RPGInventorySystem.Modules; 
using RPGInventorySystem.Items;

namespace RPGInventorySystem.Builders
{
    public class WeaponBuilder
    {
        private string _name;
        private int _damage;
        private ItemRarity _rarity = ItemRarity.Common; // Редкость по умолчанию

        public WeaponBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public WeaponBuilder WithDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public WeaponBuilder WithRarity(ItemRarity rarity)
        {
            _rarity = rarity;
            return this;
        }

        public Weapon Build()
        {
            if (string.IsNullOrEmpty(_name) || _damage <= 0)
            {
                throw new System.InvalidOperationException("Невозможно создать оружие без имени или с отрицательным уроном.");
            }
            return new Weapon(_name, _damage, _rarity);
        }
    }
}