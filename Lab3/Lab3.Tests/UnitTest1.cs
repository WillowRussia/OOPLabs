using FoodDeliveryService;
using FoodDeliveryService.Builder;
using FoodDeliveryService.Cost;
using FoodDeliveryService.Observer;
using FoodDeliveryService.Strategy; 


public class FoodDeliverySystemTests
{
    //Тесты для Dish

    [Fact]
    public void CreateDish_WithNullName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Dish(null, 100m));
    }
    
    [Fact]
    public void CreateDish_WithEmptyName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Dish("", 100m));
    }

    [Fact]
    public void CreateDish_WithWhitespaceName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Dish(" ", 100m));
    }

    [Fact]
    public void CreateDish_WithZeroPrice_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Dish("Борщ", 0m));
    }

    [Fact]
    public void CreateDish_WithNegativePrice_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Dish("Борщ", -100m));
    }
    
    [Fact]
    public void CreateDish_WithValidParameters_Succeeds()
    {
        var name = "Стейк";
        var price = 1200m;
        var dish = new Dish(name, price);
        Assert.NotNull(dish);
        Assert.Equal(name, dish.Name);
        Assert.Equal(price, dish.Price);
    }

    //Тесты для OrderBuilder

    [Fact]
    public void BuildOrder_WithNoDishes_ThrowsInvalidOperationException()
    {
        var builder = new OrderBuilder("client-01");
        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void BuildOrder_WithValidData_CreatesOrderWithCorrectProperties()
    {
        var pizza = new Dish("Пицца", 500m);
        var order = new OrderBuilder("client-02")
            .AddDish(pizza)
            .WithDeliveryStrategy(new ExpressDeliveryStrategy())
            .Build();

        Assert.Equal("client-02", order.CustomerId);
        Assert.IsType<ExpressDeliveryStrategy>(order.DeliveryStrategy);
        Assert.Single(order.Dishes);
    }

    //Тесты для Order State

    [Fact]
    public void ProceedOrder_ThroughAllStates_ChangesStatusCorrectly()
    {
        var pizza = new Dish("Пицца", 500m);
        var order = new OrderBuilder("client-03").AddDish(pizza).Build();

        Assert.Equal("В ожидании", order.CurrentStatus.StatusName);
        order.ProceedToNextState();
        Assert.Equal("Готовится", order.CurrentStatus.StatusName);
        order.ProceedToNextState();
        Assert.Equal("В доставке", order.CurrentStatus.StatusName);
        order.ProceedToNextState();
        Assert.Equal("Выполнен", order.CurrentStatus.StatusName);
    }

    [Fact]
    public void CancelOrder_FromPendingState_ChangesStatusToCanceled()
    {
        var pizza = new Dish("Пицца", 500m);
        var order = new OrderBuilder("client-04").AddDish(pizza).Build();
        order.CurrentStatus.Cancel();
        Assert.Equal("Отменен", order.CurrentStatus.StatusName);
    }

    [Fact]
    public void AddDish_ToOrderNotInPendingState_ThrowsInvalidOperationException()
    {
        var pizza = new Dish("Пицца", 500m);
        var drink = new Dish("Кола", 100m);
        var order = new OrderBuilder("client-05").AddDish(pizza).Build();
        order.ProceedToNextState(); // Статус -> Готовится
        Assert.Throws<InvalidOperationException>(() => order.AddDish(drink));
    }
    
    //есты для CostCalculator и Decorator

    [Fact]
    public void CalculateCost_WithDiscountAndTax_AppliesDecoratorsCorrectly()
    {
        var pizza = new Dish("Пицца '4 сыра'", 700m);
        var order = new OrderBuilder("client-06")
            .AddDish(pizza)
            .WithDeliveryStrategy(new StandardDeliveryStrategy()) // +150
            .WithDiscount(100m) // -100
            .WithTax(0.1m) // +10%
            .Build();
            
        // Расчет: ((700 - 100) * 1.1) + 150 = 660 + 150 = 810
        decimal expectedCost = 810.00m;
        Assert.Equal(expectedCost, order.CalculateTotalCost());
    }

    [Fact]
    public void CalculateCost_WhenDecoratorOrderDiffers_ProducesDifferentResults()
    {
        var dish = new Dish("Дорогое блюдо", 1000m);
        var order = new Order("client-07", new StandardDeliveryStrategy(), new BaseCostCalculator());
        order.AddDish(dish);
        
        // начала скидка 200, потом налог 10%
        ICostCalculator discountFirst = new TaxDecorator(new DiscountDecorator(new BaseCostCalculator(), 200m), 0.1m);
        order.CostCalculator = discountFirst;
        // (1000 - 200) * 1.1 = 880
        Assert.Equal(880m, order.CostCalculator.CalculateCost(order));

        // Сначала налог 10%, потом скидка 200
        ICostCalculator taxFirst = new DiscountDecorator(new TaxDecorator(new BaseCostCalculator(), 0.1m), 200m);
        order.CostCalculator = taxFirst;
        // (1000 * 1.1) - 200 = 900
        Assert.Equal(900m, order.CostCalculator.CalculateCost(order));
    }

    //Тесты для Observer

    [Fact]
    public void ChangeOrderStatus_WhenObserverIsAttached_NotifiesObserver()
    {
        var pizza = new Dish("Пицца", 500m);
        var order = new OrderBuilder("client-08").AddDish(pizza).Build();
        var notifier = new EmailNotifier();
        order.Attach(notifier);

        Assert.Empty(notifier.LastNotification);
        order.ProceedToNextState(); // Статус -> Готовится
        Assert.Contains("Готовится", notifier.LastNotification);
    }

    [Fact]
    public void ChangeOrderStatus_AfterObserverIsDetached_DoesNotNotifyObserver()
    {
        var pizza = new Dish("Пицца", 500m);
        var order = new OrderBuilder("client-09").AddDish(pizza).Build();
        var notifier = new EmailNotifier();
        order.Attach(notifier);

        order.ProceedToNextState(); // Статус -> Готовится
        var notificationBeforeDetach = notifier.LastNotification;
        
        order.Detach(notifier);
        order.ProceedToNextState(); // Статус -> В доставке

        // Уведомление не должно было измениться
        Assert.Equal(notificationBeforeDetach, notifier.LastNotification);
    }
}