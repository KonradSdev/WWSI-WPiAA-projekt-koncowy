using CafeOrderManagerApp.Models;
namespace CafeOrderManagerApp.DesignPatterns.Behavioral;
public class CustomerScreen : IOrderObserver
{
    private readonly ListView _clientList;
    private readonly Label _headerLabel;
    private System.Windows.Forms.Timer _resetTimer;

    public CustomerScreen(ListView clientList, Label headerLabel)
    {
        _clientList = clientList;
        _headerLabel = headerLabel;

        // Inicjalizacja Timera
        _resetTimer = new System.Windows.Forms.Timer();
        _resetTimer.Interval = 10000; // 10 sekund
        _resetTimer.Tick += ResetLabel;
    }

    public void Update(Order order)
    {
        if (_clientList.InvokeRequired)
        {
            _clientList.Invoke(new Action(() => Update(order)));
            return;
        }

        // Interesuje nas tylko moment, gdy zamówienie jest gotowe
        if (order.Status == "Gotowe do odbioru")
        {
            // Aktualizujemy wielki napis dla klienta
            _headerLabel.Text = $"ZAMÓWIENIE #{order.Id} GOTOWE DO ODBIORU!";
            _headerLabel.BackColor = Color.Yellow;
            _headerLabel.ForeColor = Color.Black;

            // Restartujemy timer (jeśli już odliczał dla innego zamówienia, zacznie od nowa)
            _resetTimer.Stop();
            _resetTimer.Start();
        }
    }

    private void ResetLabel(object sender, EventArgs e)
    {
        _resetTimer.Stop();
        _headerLabel.Text = "Oczekujące zamówienia";
        _headerLabel.BackColor = Color.Transparent;
        _headerLabel.ForeColor = Color.Black;
    }
}
