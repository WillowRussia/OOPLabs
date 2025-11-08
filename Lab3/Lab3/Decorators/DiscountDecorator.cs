namespace FoodDeliveryService.Cost
{
    public class DiscountDecorator : ICostCalculator
    {
        private readonly ICostCalculator _calculator;
        private readonly decimal _discountAmount;

        public DiscountDecorator(ICostCalculator calculator, decimal discountAmount)
        {
            _calculator = calculator;
            _discountAmount = discountAmount;
        }

        public decimal CalculateCost(Order order)
        {
            decimal originalCost = _calculator.CalculateCost(order);
            return Math.Max(0, originalCost - _discountAmount);
        }
    }
}