namespace FoodDeliveryService.Cost
{

    public class TaxDecorator : ICostCalculator
    {
        private readonly ICostCalculator _calculator;
        private readonly decimal _taxRate;

        public TaxDecorator(ICostCalculator calculator, decimal taxRate)
        {
            _calculator = calculator;
            _taxRate = taxRate;
        }

        public decimal CalculateCost(Order order)
        {
            decimal originalCost = _calculator.CalculateCost(order);
            return originalCost * (1 + _taxRate);
        }
    }
}