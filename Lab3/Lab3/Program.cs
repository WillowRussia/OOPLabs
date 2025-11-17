using FoodDeliveryService;
using FoodDeliveryService.Builder;
using FoodDeliveryService.Observer;
using FoodDeliveryService.State;
using FoodDeliveryService.Strategy;
using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

Console.WriteLine("=== Система Управления Заказами Службы Доставки ===");

var pizza = new Dish("Пицца 'Маргарита'", 550.00m);
var pasta = new Dish("Паста 'Карбонара'", 480.50m);
var salad = new Dish("Салат 'Цезарь'", 320.00m);
var drink = new Dish("Лимонад", 150.00m);

var emailNotifier = new EmailNotifier();

Console.WriteLine("\n--- Сценарий 1: Заказ с экспресс-доставкой и скидкой ---");

var orderBuilder = new OrderBuilder("client-001");
var complexOrder = orderBuilder
    .AddDish(pizza)
    .AddDish(drink)
    .WithDeliveryStrategy(new ExpressDeliveryStrategy())
    .WithDiscount(100.00m) 
    .WithTax(0.05m)
    .Build();

complexOrder.Attach(emailNotifier);

Console.WriteLine($"Итоговая стоимость заказа: {complexOrder.CalculateTotalCost():C}");

Console.WriteLine("\nНачинаем обработку заказа...");
Console.WriteLine($"Текущий статус: «{complexOrder.CurrentStatus.StatusName}»");

complexOrder.ProceedToNextState();
complexOrder.ProceedToNextState();
complexOrder.ProceedToNextState();
complexOrder.ProceedToNextState();

Console.WriteLine("\n--- Сценарий 2: Стандартный заказ ---");

var simpleOrder = new OrderBuilder("client-002")
    .AddDish(pasta)
    .AddDish(salad)
    .Build();

Console.WriteLine($"Итоговая стоимость заказа: {simpleOrder.CalculateTotalCost():C}");
Console.WriteLine($"Текущий статус: «{simpleOrder.CurrentStatus.StatusName}»"); 

Console.WriteLine("\n--- Сценарий 3: Отмена заказа ---");
var orderToCancel = new OrderBuilder("client-003").AddDish(pizza).Build();
Console.WriteLine($"Текущий статус: «{orderToCancel.CurrentStatus.StatusName}»"); 
(orderToCancel.CurrentStatus as PendingState)?.Cancel();

Console.WriteLine($"Новый статус: «{orderToCancel.CurrentStatus.StatusName}»"); 