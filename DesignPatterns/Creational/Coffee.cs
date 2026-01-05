using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;
public class Coffee : IProduct
{
    public string Name => "Kawa Espresso";
    public double GetPrice() => 8.0;
    public string GetDescription() => "Kawa Espresso";
}
