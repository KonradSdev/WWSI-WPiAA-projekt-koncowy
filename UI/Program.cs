using System.Windows.Forms;
using CafeOrderManagerApp.UI; // Pamiętaj o użyciu przestrzeni nazw dla Twojego formularza

namespace CafeOrderManagerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
