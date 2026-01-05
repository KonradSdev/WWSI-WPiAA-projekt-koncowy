using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Structural;
public class WhipCreamDecorator : ProductDecorator
{
    public WhipCreamDecorator(IProduct product) : base(product) { }

    public override double GetPrice() => base.GetPrice() + 2.0; // Dodatkowa opłata
    public override string GetDescription() => base.GetDescription() + " + Bita Śmietana";
}
