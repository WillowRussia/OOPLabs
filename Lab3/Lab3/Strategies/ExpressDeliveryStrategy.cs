namespace FoodDeliveryService.Strategy
{
    public class ExpressDeliveryStrategy : IDeliveryCostStrategy
    {
        public decimal CalculateCost(Order order)
        {
            return 300m;
        }
    }
}