namespace FoodDeliveryService.State
{
    public interface IOrderStatus
    {

        string StatusName { get; }

        void ProceedToNextState();
        void Cancel();
    }
}