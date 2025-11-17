using FoodDeliveryService.Cost;
using FoodDeliveryService.Strategy;

namespace FoodDeliveryService.Builder
{
    public class OrderBuilder
    {
        private readonly string _customerId;
        private readonly List<Dish> _dishes = new();
        private IDeliveryCostStrategy _deliveryStrategy = new StandardDeliveryStrategy();
        private decimal _discount = 0;
        private decimal _taxRate = 0;

        public OrderBuilder(string customerId)
        {
            _customerId = customerId;
        }

        public OrderBuilder AddDish(Dish dish)
        {
            _dishes.Add(dish);
            return this;
        }

        public OrderBuilder WithDeliveryStrategy(IDeliveryCostStrategy strategy)
        {
            _deliveryStrategy = strategy;
            return this;
        }

        public OrderBuilder WithDiscount(decimal discount)
        {
            if (discount < 0) throw new ArgumentException("Скидка не может быть отрицательной.");
            _discount = discount;
            return this;
        }

        public OrderBuilder WithTax(decimal taxRate)
        {
            if (taxRate < 0) throw new ArgumentException("Налоговая ставка не может быть отрицательной.");
            _taxRate = taxRate;
            return this;
        }

        public Order Build()
        {
            if (_dishes.Count == 0)
            {
                throw new InvalidOperationException("Нельзя создать заказ без блюд.");
            }

            // Настройка калькулятора стоимости с помощью Декораторов
            ICostCalculator costCalculator = new BaseCostCalculator();
            if (_discount > 0)
            {
                costCalculator = new DiscountDecorator(costCalculator, _discount);
            }
            if (_taxRate > 0)
            {
                costCalculator = new TaxDecorator(costCalculator, _taxRate);
            }
            
            var order = new Order(_customerId, _deliveryStrategy, costCalculator);

            foreach (var dish in _dishes)
            {
                order.AddDish(dish);
            }

            return order;
        }
    }
}