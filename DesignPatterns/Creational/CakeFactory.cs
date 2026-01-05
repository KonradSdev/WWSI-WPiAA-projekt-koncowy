using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;
public class CakeFactory : ProductFactory
{
    public override IProduct CreateProduct() => new Cake();
}
