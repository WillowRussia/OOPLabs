namespace RPGInventorySystem.Modules
{
    public class Player
    {
        public int Health { get; set; } = 100;
        public int Mana { get; set; } = 50;
        public int Attack { get; private set; } = 5;
        public int Defense { get; private set; } = 0;

        public Inventory Inventory { get; }

        public Player()
        {
            Inventory = new Inventory();
        }

        public void AddAttack(int points) => Attack += points;
        public void AddDefense(int points) => Defense += points;
    }
}