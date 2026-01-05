using CafeOrderManagerApp.Models;

namespace CafeOrderManagerApp.DesignPatterns.Structural;
public class MilkDecorator : ProductDecorator
{
    public MilkDecorator(IProduct product) : base(product) { }

    public override double GetPrice() => base.GetPrice() + 1.5; // Dodatkowa opłata
    public override string GetDescription() => base.GetDescription() + " + Mleko";
}
