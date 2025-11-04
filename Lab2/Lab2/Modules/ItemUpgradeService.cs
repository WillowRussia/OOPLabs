namespace RPGInventorySystem.Modules
{
    public class ItemUpgradeService
    {
        public Weapon UpgradeWeapon(Weapon weapon, int extraDamage, ItemRarity newRarity)
        {
            var newName = weapon.Name + " +1";
            return new Weapon(newName, weapon.Damage + extraDamage, newRarity);
        }

        public Armor UpgradeArmor(Armor armor, int extraDefense, ItemRarity newRarity)
        {
            var newName = armor.Name + " +1";
            return new Armor(newName, armor.Defense + extraDefense, newRarity);
        }
    }
}