namespace CafeOrderManagerApp.Models;
public class PricingSystem
{
    public double CalculateTotal(Order order)
    {
        double total = order.Products.Sum(p => p.GetPrice());
        Console.WriteLine($"[PricingSystem] Łączna kwota zamówienia #{order.Id}: {total:C}");
        return total;
    }
}
