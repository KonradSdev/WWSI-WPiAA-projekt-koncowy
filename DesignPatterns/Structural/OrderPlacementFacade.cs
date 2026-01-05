using CafeOrderManagerApp.Models;
using CafeOrderManagerApp.DesignPatterns.Creational;
namespace CafeOrderManagerApp.DesignPatterns.Structural;
public class OrderPlacementFacade
{
    public (Order Order, double TotalPrice, string Description) PlaceAndLogOrder(List<IProduct> products)
    {
        var orderManager = OrderManager.GetInstance();

        // Zlecenie utworzenia zamówienia w systemie
        // To automatycznie wywoła NotifyObservers w OrderManagerze
        Order newOrder = orderManager.PlaceNewOrder(new List<IProduct>(products));

        // Obliczenia kwot i przygotowanie opisu (logika przeniesiona z GUI)
        double totalAmount = newOrder.Products.Sum(p => p.GetPrice());
        string productDetails = string.Join(", ", newOrder.Products.Select(p => p.GetDescription()));

        // Zwracamy krotkę (tuple), aby MainForm miał dane do wyświetlenia w logach
        return (newOrder, totalAmount, productDetails);
    }
}
