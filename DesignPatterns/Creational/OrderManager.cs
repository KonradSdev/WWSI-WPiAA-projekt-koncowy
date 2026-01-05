using System.Collections.Generic;
using CafeOrderManagerApp.DesignPatterns.Behavioral;
using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;

public class OrderManager
{
    private static OrderManager? _instance;
    private List<Order> _pendingOrders = new List<Order>();
    private int _nextOrderId = 1;

    // Lista globalnych obserwatorów
    private List<IOrderObserver> _observers = new List<IOrderObserver>();

    private OrderManager() { }

    public static OrderManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new OrderManager();
            Console.WriteLine("SYSTEM: OrderManager zainicjalizowany (Singleton).");
        }
        return _instance;
    }

    // Metoda do rejestracji obserwatorów (np. BaristaDisplay, CustomerScreen)
    public void Attach(IOrderObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    // Metoda do wyrejestrowania (opcjonalnie)
    public void Detach(IOrderObserver observer)
    {
        _observers.Remove(observer);
    }

    // Metoda powiadamiająca wszystkich o zmianie statusu konkretnego zamówienia
    public void NotifyObservers(Order order)
    {
        foreach (var observer in _observers)
        {
            observer.Update(order);
        }
    }

    public Order PlaceNewOrder(List<IProduct> products)
    {
        var newOrder = new Order(_nextOrderId++, products);
        _pendingOrders.Add(newOrder);

        // Powiadamiamy obserwatorów o nowym zamówieniu (status domyślny)
        NotifyObservers(newOrder);

        return newOrder;
    }

    public Order? GetOrder(int id)
    {
        return _pendingOrders.FirstOrDefault(o => o.Id == id);
    }

    public void UpdateOrderStatus(int orderId, string newStatus)
    {
        var order = GetOrder(orderId);
        if (order != null)
        {
            order.Status = newStatus;
            NotifyObservers(order);    
        }
    }
    public void CancelOrder(int orderId)
    {
        var order = GetOrder(orderId);
        if (order != null)
        {
            order.Status = "Anulowano";
            NotifyObservers(order); // MainForm dopisze to do logów
            _pendingOrders.Remove(order);
        }
    }
}