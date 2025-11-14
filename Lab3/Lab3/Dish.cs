namespace FoodDeliveryService
{
    public class Dish
    {
        public string Name { get; }
        public decimal Price { get; }

        public Dish(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название блюда не может быть пустым.", nameof(name));
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Цена должна быть положительной.");

            Name = name;
            Price = price;
        }
    }
}