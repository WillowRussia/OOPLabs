namespace FoodDeliveryService.State
{
    public class PreparationState : IOrderStatus
    {
        private readonly Order _order;
        public string StatusName => "Готовится";

        public PreparationState(Order order)
        {
            _order = order;
        }

        public void ProceedToNextState()
        {
            _order.TransitionToState(new InDeliveryState(_order));
        }

        public void Cancel()
        {
            throw new InvalidOperationException("Нельзя отменить заказ, который уже находится в процессе приготовления.");
        }
    }
}