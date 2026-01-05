using System.Threading;
using CafeOrderManagerApp.Models;
using CafeOrderManagerApp.DesignPatterns.Creational;
namespace CafeOrderManagerApp.DesignPatterns.Structural;
public class OrderFulfillmentFacade
{
    private readonly OrderManager _manager = OrderManager.GetInstance();
    private readonly PricingSystem _pricing = new PricingSystem();

    public void ProcessAndCompleteOrder(int orderId)
    {
        var manager = OrderManager.GetInstance();

        // Logika biznesowa (np. zmiana statusu na przygotowywanie)
        manager.UpdateOrderStatus(orderId, "Przygotowywanie");

        // Możliwość dodania innych kroków procesu, których nie chcemy widzieć w GUI:

        Console.WriteLine($"[Fasada] Zamówienie #{orderId} przekazane do realizacji.");
    }
}
