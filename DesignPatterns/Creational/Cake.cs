using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;

public class Cake : IProduct
{
    public string Name => "Ciasto Czekoladowe";
    public double GetPrice() => 12.5;
    public string GetDescription() => "Ciasto Czekoladowe";
}
