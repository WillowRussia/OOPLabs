using FoodDeliveryService.State;
using FoodDeliveryService.Observer;
using FoodDeliveryService.Strategy;
using FoodDeliveryService.Cost;

namespace FoodDeliveryService
{
    public class Order : ISubject
    {
        private readonly List<IObserver> _observers = new();
        private readonly List<Dish> _dishes = new();

        public IReadOnlyList<Dish> Dishes => _dishes.AsReadOnly();
        public IOrderStatus CurrentStatus { get; private set; }
        public IDeliveryCostStrategy DeliveryStrategy { get; set; }
        public ICostCalculator CostCalculator { get; set; }
        public string CustomerId { get; }

        public Order(string customerId, IDeliveryCostStrategy deliveryStrategy, ICostCalculator costCalculator)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("Идентификатор клиента не может быть пустым.", nameof(customerId));

            CustomerId = customerId;
            DeliveryStrategy = deliveryStrategy ?? throw new ArgumentNullException(nameof(deliveryStrategy));
            CostCalculator = costCalculator ?? throw new ArgumentNullException(nameof(costCalculator));
            CurrentStatus = new PendingState(this); // Начальное состояние
        }

        public void AddDish(Dish dish)
        {
            if (CurrentStatus is not PendingState)
            {
                throw new InvalidOperationException("Нельзя добавлять блюда в заказ, который не находится в состоянии ожидания.");
            }
            _dishes.Add(dish);
        }

        public void TransitionToState(IOrderStatus newState)
        {
            CurrentStatus = newState;
            Console.WriteLine($"Статус заказа изменен на: «{newState.StatusName}»"); 
            Notify();
        }

        public void ProceedToNextState()
        {
            CurrentStatus.ProceedToNextState();
        }

        public decimal CalculateTotalCost()
        {
            decimal totalCost = CostCalculator.CalculateCost(this);
            totalCost += DeliveryStrategy.CalculateCost(this);
            return totalCost;
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}