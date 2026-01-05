using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;
public class CoffeeFactory : ProductFactory
{
    public override IProduct CreateProduct() => new Coffee();
}
