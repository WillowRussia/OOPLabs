namespace FoodDeliveryService.State
{
    public class PendingState : IOrderStatus
    {
        private readonly Order _order;
        public string StatusName => "В ожидании";

        public PendingState(Order order)
        {
            _order = order;
        }

        public void ProceedToNextState()
        {
            _order.TransitionToState(new PreparationState(_order));
        }

        public void Cancel()
        {
             _order.TransitionToState(new CanceledState(_order));
        }
    }
}