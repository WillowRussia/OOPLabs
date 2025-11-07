namespace FoodDeliveryService.Strategy
{
    public class StandardDeliveryStrategy : IDeliveryCostStrategy
    {
        public decimal CalculateCost(Order order)
        {
            return 150m;
        }
    }
}