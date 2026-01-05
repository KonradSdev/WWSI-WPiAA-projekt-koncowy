using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Behavioral;
public interface IOrderObserver
{
    void Update(Order order);
}
