namespace FoodDeliveryService.State
{
    public class CompletedState : IOrderStatus
    {
        private readonly Order _order;
        public string StatusName => "Выполнен";

        public CompletedState(Order order)
        {
            _order = order;
        }

        public void ProceedToNextState()
        {
            Console.WriteLine("Заказ уже выполнен.");
        }

        public void Cancel()
        {
            throw new InvalidOperationException("Нельзя отменить выполненный заказ.");
        }
    }
}