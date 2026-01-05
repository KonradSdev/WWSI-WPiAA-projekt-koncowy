namespace CafeOrderManagerApp.Models;

public interface IProduct
{
    string Name { get; }
    double GetPrice();
    string GetDescription();
}
