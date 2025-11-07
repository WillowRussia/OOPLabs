namespace FoodDeliveryService.Strategy
{
    public interface IDeliveryCostStrategy
    {
        decimal CalculateCost(Order order);
    }
}