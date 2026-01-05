using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Creational;
public class TeaFactory : ProductFactory
{
    public override IProduct CreateProduct() => new Tea();
}
