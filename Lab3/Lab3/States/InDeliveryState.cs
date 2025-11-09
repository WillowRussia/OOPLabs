namespace FoodDeliveryService.State
{
    public class InDeliveryState : IOrderStatus
    {
        private readonly Order _order;
        public string StatusName => "В доставке";

        public InDeliveryState(Order order)
        {
            _order = order;
        }

        public void ProceedToNextState()
        {
            _order.TransitionToState(new CompletedState(_order));
        }

        public void Cancel()
        {
            throw new InvalidOperationException("Нельзя отменить заказ, который уже находится в доставке.");
        }
    }
}