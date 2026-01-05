using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;

public class Tea : IProduct
{
    public string Name => "Herbata";
    public double GetPrice() => 8.5;
    public string GetDescription() => "Herbata liœciasta";
}
