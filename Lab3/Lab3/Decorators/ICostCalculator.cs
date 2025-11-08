namespace FoodDeliveryService.Cost
{
    public interface ICostCalculator
    {
        decimal CalculateCost(Order order);
    }
}