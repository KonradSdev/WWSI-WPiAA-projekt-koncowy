using System.Collections.Generic;
using System.Linq;
using CafeOrderManagerApp.DesignPatterns.Behavioral;

namespace CafeOrderManagerApp.Models;
public class Order
{
    public int Id { get; }
    public List<IProduct> Products { get; }

    private List<IOrderObserver> _observers = new List<IOrderObserver>();
    private string _status = "New";

    public string Status
    {
        get => _status;
        set
        {
            if (_status != value)
            {
                _status = value;
                NotifyObservers(); // Powiadom wszystkich obserwatorów o zmianie
            }
        }
    }

    public Order(int id, List<IProduct> products)
    {
        Id = id;
        Products = products;
    }

    public void Attach(IOrderObserver observer) => _observers.Add(observer);
    public void Detach(IOrderObserver observer) => _observers.Remove(observer);

    private void NotifyObservers()
    {
        Console.WriteLine($"[Order #{Id}] Zmiana Statusu: {Status}");
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}
