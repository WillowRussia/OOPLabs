namespace FoodDeliveryService.Observer
{
    public class EmailNotifier : IObserver
    {
        public string LastNotification { get; private set; } = string.Empty;

        public void Update(Order order)
        {
            LastNotification = $"Email для {order.CustomerId}: Статус вашего заказа изменен на «{order.CurrentStatus.StatusName}»."; // Стало
            Console.WriteLine(LastNotification);
        }
    }
}