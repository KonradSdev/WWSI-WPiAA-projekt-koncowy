using CafeOrderManagerApp.Models;
using CafeOrderManagerApp.DesignPatterns.Creational;
using CafeOrderManagerApp.DesignPatterns.Structural;
using CafeOrderManagerApp.DesignPatterns.Behavioral;

namespace CafeOrderManagerApp.UI;

public partial class MainForm : Form, IOrderObserver
{
    private List<IProduct> _currentOrderProducts = new List<IProduct>();

    // Obserwatorzy
    private BaristaDisplay _baristaObserver;
    private CustomerScreen _customerObserver;

    private Dictionary<string, ProductFactory> _availableFactories;
    private ContextMenuStrip contextMenuPositions;

    public MainForm()
    {
        // Inicjalizacja komponentów, fabryk oraz obserwatorów z dostępem do kontrolek
        InitializeComponent();
        InitializeFactories();
        PopulateProductList();
        InitializeOrderPositionsMenu();

        _baristaObserver = new BaristaDisplay(listAwaitingOrdersBarista);
        _customerObserver = new CustomerScreen(listAwaitingOrdersClient, lblPendingOrders);

        // Subskrypcja w OrderManager
        var orderManager = OrderManager.GetInstance();
        orderManager.Attach(_baristaObserver);
        orderManager.Attach(_customerObserver);
        orderManager.Attach(this); // Ten formularz loguje zmiany do txtStatusLog
    }

    private void InitializeFactories()
    {
        // Przygotowanie dostępnych fabryk
        _availableFactories = new Dictionary<string, ProductFactory>
        {
            {"Kawa Espresso", new CoffeeFactory()},
            {"Ciasto Czekoladowe", new CakeFactory()},
            {"Herbata", new TeaFactory()},
        };
    }

    private void PopulateProductList()
    {
        // Przygotowanie listy dostępnych produktów
        cmbProductSelection.Items.Clear();
        cmbProductSelection.Items.AddRange(_availableFactories.Keys.ToArray());
        if (cmbProductSelection.Items.Count > 0) cmbProductSelection.SelectedIndex = 0;
    }

    // Metoda pozwalająca na usunięcie poszczególnych pozycji z listy do zamówienia poprzez klikniecie prawym przyciskiem
    private void InitializeOrderPositionsMenu()
    {
        contextMenuPositions = new ContextMenuStrip();

        // Tworzymy element menu "Usuń"
        ToolStripMenuItem deleteItem = new ToolStripMenuItem("Usuń pozycję");
        deleteItem.Click += RemoveProduct_Click; // Podpięcie zdarzenia kliknięcia

        // Dodajemy element do menu
        contextMenuPositions.Items.Add(deleteItem);

        // Przypisujemy menu do listy
        listOrderPositions.ContextMenuStrip = contextMenuPositions;
    }

    // Tworzenie logów
    private void UpdateGuiStatus(string message)
    {
        if (txtStatusLog.InvokeRequired)
        {
            txtStatusLog.Invoke(new Action(() => UpdateGuiStatus(message)));
            return;
        }
        txtStatusLog.AppendText(message + Environment.NewLine);
    }

    private void RemoveProduct_Click(object sender, EventArgs e)
    {
        if (listOrderPositions.SelectedItems.Count > 0)
        {
            ListViewItem item = listOrderPositions.SelectedItems[0];

            // Wyświetlamy potwierdzenie
            var result = MessageBox.Show($"Usunąć {item.Text} z listy?", "Potwierdzenie", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Usuwamy z listy obiektów (zmiennej)
                if (item.Tag is IProduct product)
                {
                    _currentOrderProducts.Remove(product);
                }

                // Usuwamy z widoku
                listOrderPositions.Items.Remove(item);
                UpdateGuiStatus($"[USUNIĘTO] {item.Text}");
            }
        }
    }

    // Interfejs Obserwatora
    public void Update(Order order)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => Update(order)));
            return;
        }

        // Synchronizujemy tekst statusu na liście klienta (kolory i detale robią dedykowani obserwatorzy)
        ManageOrdersList(order.Id.ToString(), "Update", order.Status);

        UpdateGuiStatus($"[POWIADOMIENIE] Zamówienie #{order.Id} zmieniło status na: {order.Status}");
    }

    private void ManageOrdersList(string orderId, string method, string status)
    {
        switch (method)
        {
            case "New":
                ListViewItem newItem = new ListViewItem(orderId);
                newItem.SubItems.Add(status);
                listAwaitingOrdersClient.Items.Add(newItem);
                break;

            case "Update":
                foreach (ListViewItem item in listAwaitingOrdersClient.Items)
                {
                    if (item.Text == orderId)
                    {
                        // Aktualizacja tekstu statusu w drugiej kolumnie
                        item.SubItems[1].Text = status;

                        // Logika kolorowania na zielono
                        if (status == "Gotowe do odbioru")
                        {
                            item.BackColor = Color.LightGreen; // Cały wiersz na zielono
                            item.UseItemStyleForSubItems = true;
                        }
                        else if (status == "Przygotowywanie")
                        {
                            item.BackColor = Color.LightBlue; // niebieski dla przygotowywanych
                        }
                        else
                        {
                            item.BackColor = SystemColors.Window; // Powrót do domyślnego koloru (np. po anulowaniu)
                        }

                        break;
                    }
                }

                break;
                // Usuwanie z listy zamówień np. po odbiorze
            case "Delete":
                foreach (ListViewItem itm in listAwaitingOrdersClient.Items)
                {
                    if (itm.Text == orderId) { listAwaitingOrdersClient.Items.Remove(itm); break; }
                }
                break;
        }
    }

    private void btnPlaceOrder_Click_1(object sender, EventArgs e)
    {
        if (_currentOrderProducts.Count == 0)
        {
            MessageBox.Show("Dodaj produkty przed złożeniem zamówienia.");
            return;
        }

        // Użycie Fasady
        var placementFacade = new OrderPlacementFacade();
        var result = placementFacade.PlaceAndLogOrder(_currentOrderProducts);

        // Wyciągamy dane z wyniku fasady
        Order newOrder = result.Order;
        double totalAmount = result.TotalPrice;
        string productDetails = result.Description;

        UpdateGuiStatus("========================================");
        UpdateGuiStatus($"[NOWE ZAMÓWIENIE] ID: #{newOrder.Id}");
        UpdateGuiStatus($"[POZYCJE] {productDetails}");
        UpdateGuiStatus($"[SUMA DO ZAPŁATY] {totalAmount:C}");
        UpdateGuiStatus("========================================");

        // Dodajemy wiersze do widoków list (ListView). Status początkowy to "Oczekiwanie"
        ManageOrdersList(newOrder.Id.ToString(), "New", "Oczekiwanie");
        ManageBaristaList(newOrder.Id.ToString(), "New", productDetails);

        // Czyścimy listę roboczą (przygotowanie pod następnego klienta)
        _currentOrderProducts.Clear();
        listOrderPositions.Items.Clear();
    }

    private void btnFulfillOrder_Click_1(object sender, EventArgs e)
    {
        if (listAwaitingOrdersBarista.SelectedItems.Count > 0)
        {
            var facade = new OrderFulfillmentFacade();
            foreach (ListViewItem selectedItem in listAwaitingOrdersBarista.SelectedItems)
            {
                if (int.TryParse(selectedItem.Text, out int orderId))
                {
                    // Wywołujemy tylko metodę w Managerze, wywołanie poprzez Fasadę. 
                    // Obserwatorzy sami odświeżą kolory i statusy na listach.
                    facade.ProcessAndCompleteOrder(orderId);

                    UpdateGuiStatus($"[SYSTEM] Fasada uruchomiła proces dla zamówienia #{orderId}.");
                }
            }
        }
        else
        {
            MessageBox.Show("Zaznacz zamówienie do realizacji.");
        }
    }

    private void btnOrderReady_Click(object sender, EventArgs e)
    {
        if (listAwaitingOrdersBarista.SelectedItems.Count > 0)
        {
            ListViewItem[] selectedItems = new ListViewItem[listAwaitingOrdersBarista.SelectedItems.Count];
            listAwaitingOrdersBarista.SelectedItems.CopyTo(selectedItems, 0);

            foreach (ListViewItem item in selectedItems)
            {
                if (int.TryParse(item.Text, out int orderId))
                {
                    // Manager powiadomi CustomerScreen (lista zrobi się zielona) i BaristaDisplay
                    OrderManager.GetInstance().UpdateOrderStatus(orderId, "Gotowe do odbioru");

                    // Usuwamy tylko z listy baristy, bo on skończył pracę
                    ManageBaristaList(orderId.ToString(), "Delete");
                }
            }
        }
    }

    private void btnPickupOrder_Click(object sender, EventArgs e)
    {
        if (listAwaitingOrdersClient.SelectedItems.Count > 0)
        {
            // Tworzymy kopię, aby bezpiecznie usuwać podczas pętli
            ListViewItem[] selectedItems = new ListViewItem[listAwaitingOrdersClient.SelectedItems.Count];
            listAwaitingOrdersClient.SelectedItems.CopyTo(selectedItems, 0);

            foreach (ListViewItem item in selectedItems)
            {
                string orderId = item.Text;
                string currentStatus = item.SubItems[1].Text.Trim();

                // Sprawdzamy status (używamy StringComparison, aby uniknąć błędów z wielkością liter)
                // Dodatkowo sprawdzamy, czy wiersz jest zielony (dodatkowe zabezpieczenie)
                if (currentStatus.Equals("Gotowe do odbioru", StringComparison.OrdinalIgnoreCase) ||
                    item.BackColor == Color.LightGreen)
                {
                    // USUNIĘCIE Z LISTY
                    ManageOrdersList(orderId, "Delete", "");

                    UpdateGuiStatus($"[ODBIÓR] Zamówienie #{orderId} zostało odebrane i usunięte z listy.");
                }
                else
                {
                    MessageBox.Show($"Zamówienie #{orderId} nie może zostać odebrane, ponieważ nie jest jeszcze gotowe.");
                }
            }
        }
        else
        {
            MessageBox.Show("Proszę zaznaczyć zamówienie na liście klienta.");
        }
    }

    // Pomocnicza metoda resetująca wygląd etykiety
    private void ResetCustomerDisplay()
    {
        lblPendingOrders.Text = "Oczekujące zamówienia";
        lblPendingOrders.BackColor = Color.Transparent;
        lblPendingOrders.ForeColor = Color.Black;
    }

    private void ManageBaristaList(string orderId, string method, string products = "")
    {
        switch (method)
        {
            case "New":
                ListViewItem item = new ListViewItem(orderId);
                item.SubItems.Add(products);
                listAwaitingOrdersBarista.Items.Add(item);
                break;
            case "Delete":
                foreach (ListViewItem itm in listAwaitingOrdersBarista.Items)
                {
                    if (itm.Text == orderId) { listAwaitingOrdersBarista.Items.Remove(itm); break; }
                }
                break;
        }
    }

    // Dodawanie produktów do listy zamówienia
    private void btnAddProduct_Click(object sender, EventArgs e)
    {
        if (cmbProductSelection.SelectedItem == null) return;

        string selectedProductName = cmbProductSelection.SelectedItem.ToString();
        if (_availableFactories.TryGetValue(selectedProductName, out ProductFactory factory))
        {
            IProduct product = factory.CreateProduct();
            if (chkMilk.Checked) product = new MilkDecorator(product);
            if (chkWhipCream.Checked) product = new WhipCreamDecorator(product);

            _currentOrderProducts.Add(product);
            ListViewItem item = new ListViewItem(product.GetDescription());
            item.Tag = product;
            listOrderPositions.Items.Add(item);
        }
    }

    // Czyszczenie listy zamówienia
    private void btnClearProductsList_Click(object sender, EventArgs e)
    {
        if (listOrderPositions.Items.Count == 0) return;
        if (MessageBox.Show("Wyczyścić listę?", "Potwierdzenie", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            _currentOrderProducts.Clear();
            listOrderPositions.Items.Clear();
        }
    }
    private void btnDeleteOrder_Click(object sender, EventArgs e)
    {
        string orderIdStr = string.Empty;
        ListView activeList = null;

        // Sprawdzamy, gdzie użytkownik zaznaczył zamówienie
        if (listAwaitingOrdersClient.SelectedItems.Count > 0)
        {
            orderIdStr = listAwaitingOrdersClient.SelectedItems[0].Text;
            activeList = listAwaitingOrdersClient;
        }
        else if (listAwaitingOrdersBarista.SelectedItems.Count > 0)
        {
            orderIdStr = listAwaitingOrdersBarista.SelectedItems[0].Text;
            activeList = listAwaitingOrdersBarista;
        }

        if (string.IsNullOrEmpty(orderIdStr))
        {
            MessageBox.Show("Proszę zaznaczyć zamówienie do usunięcia.");
            return;
        }

        // Potwierdzenie
        if (MessageBox.Show($"Czy na pewno anulować zamówienie #{orderIdStr}?", "Potwierdzenie",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            if (int.TryParse(orderIdStr, out int orderId))
            {
                // Logika biznesowa - informujemy manager o anulowaniu
                OrderManager.GetInstance().CancelOrder(orderId);

                // Fizyczne usunięcie z list (bo zamówienie całkowicie znika z systemu)
                ManageOrdersList(orderIdStr, "Delete", "");
                if (listAwaitingOrdersClient.Items.Count == 0)
                {
                    ResetCustomerDisplay();
                }
                ManageBaristaList(orderIdStr, "Delete");

                UpdateGuiStatus($"[SYSTEM] Zamówienie #{orderId} zostało trwale usunięte.");
            }
        }
    }

}