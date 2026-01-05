using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Structural;
public abstract class ProductDecorator : IProduct
{
    protected IProduct _product;

    public string Name => _product.Name;

    public ProductDecorator(IProduct product)
    {
        _product = product;
    }

    public virtual double GetPrice() => _product.GetPrice();
    public virtual string GetDescription() => _product.GetDescription();
}
