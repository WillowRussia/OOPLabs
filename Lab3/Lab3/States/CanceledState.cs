namespace FoodDeliveryService.State
{
    public class CanceledState : IOrderStatus
    {
        private readonly Order _order;
        public string StatusName => "Отменен";

        public CanceledState(Order order)
        {
            _order = order;
        }

        public void ProceedToNextState()
        {
            Console.WriteLine("Заказ отменен, дальнейшие действия невозможны.");
        }

        public void Cancel()
        {
            Console.WriteLine("Заказ уже отменен.");
        }
    }
}