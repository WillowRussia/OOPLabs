namespace FoodDeliveryService.Cost
{
    public class BaseCostCalculator : ICostCalculator
    {
        public decimal CalculateCost(Order order)
        {
            return order.Dishes.Sum(d => d.Price);
        }
    }
}