using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Behavioral;
public class BaristaDisplay : IOrderObserver
{
    private readonly ListView _baristaList;

    public BaristaDisplay(ListView baristaList)
    {
        _baristaList = baristaList;
    }

    public void Update(Order order)
    {
        if (_baristaList.InvokeRequired)
        {
            _baristaList.Invoke(new Action(() => Update(order)));
            return;
        }

        // Szukamy wiersza zamówienia na liście baristy
        foreach (ListViewItem item in _baristaList.Items)
        {
            if (item.Text == order.Id.ToString())
            {
                if (order.Status == "Przygotowywanie")
                {
                    item.BackColor = Color.LightSkyBlue; // Kolorowanie aktywnego zadania
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }
                break;
            }
        }
    }
}
