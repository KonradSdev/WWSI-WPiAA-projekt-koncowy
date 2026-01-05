using System.Windows.Forms;
using System.Drawing;

namespace CafeOrderManagerApp.UI
{
    partial class MainForm
    {
    private System.ComponentModel.IContainer components = null;

        private CheckBox chkMilk;
        private CheckBox chkWhipCream;
        private Label lblBaristaView;
        private Label lblPendingOrders;
        private Button btnFulfillOrder;


        private void InitializeComponent()
        {
            chkMilk = new CheckBox();
            chkWhipCream = new CheckBox();
            lblBaristaView = new Label();
            lblPendingOrders = new Label();
            btnPlaceOrder = new Button();
            btnFulfillOrder = new Button();
            btnOrderReady = new Button();
            lblExtras = new Label();
            cmbProductSelection = new ComboBox();
            btnAddProduct = new Button();
            label1 = new Label();
            listAwaitingOrdersClient = new ListView();
            NumerZamówienia = new ColumnHeader();
            Status = new ColumnHeader();
            btnPickupOrder = new Button();
            listAwaitingOrdersBarista = new ListView();
            columnHeader1 = new ColumnHeader();
            PozycjeZam = new ColumnHeader();
            txtStatusLog = new TextBox();
            btnDeleteOrder = new Button();
            lblBaristaOrders = new Label();
            listOrderPositions = new ListView();
            PozycjeZamówienia = new ColumnHeader();
            btnClearProductsList = new Button();
            SuspendLayout();
            // 
            // chkMilk
            // 
            chkMilk.AutoSize = true;
            chkMilk.Location = new Point(198, 62);
            chkMilk.Name = "chkMilk";
            chkMilk.Size = new Size(93, 19);
            chkMilk.TabIndex = 0;
            chkMilk.Text = "Dodaj mleko";
            chkMilk.UseVisualStyleBackColor = true;
            // 
            // chkWhipCream
            // 
            chkWhipCream.AutoSize = true;
            chkWhipCream.Location = new Point(198, 87);
            chkWhipCream.Name = "chkWhipCream";
            chkWhipCream.Size = new Size(131, 19);
            chkWhipCream.TabIndex = 1;
            chkWhipCream.Text = "Dodaj bitą śmietanę";
            chkWhipCream.UseVisualStyleBackColor = true;
            // 
            // lblBaristaView
            // 
            lblBaristaView.AutoSize = true;
            lblBaristaView.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lblBaristaView.Location = new Point(20, 9);
            lblBaristaView.Name = "lblBaristaView";
            lblBaristaView.Size = new Size(116, 21);
            lblBaristaView.TabIndex = 2;
            lblBaristaView.Text = "Widok Baristy";
            // 
            // lblPendingOrders
            // 
            lblPendingOrders.Anchor = AnchorStyles.Right;
            lblPendingOrders.AutoSize = true;
            lblPendingOrders.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lblPendingOrders.Location = new Point(363, 9);
            lblPendingOrders.Name = "lblPendingOrders";
            lblPendingOrders.Size = new Size(153, 17);
            lblPendingOrders.TabIndex = 3;
            lblPendingOrders.Text = "Oczekujące zamówienia";
            // 
            // btnPlaceOrder
            // 
            btnPlaceOrder.BackColor = Color.Honeydew;
            btnPlaceOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            btnPlaceOrder.ImageAlign = ContentAlignment.BottomLeft;
            btnPlaceOrder.Location = new Point(21, 222);
            btnPlaceOrder.Name = "btnPlaceOrder";
            btnPlaceOrder.Size = new Size(201, 28);
            btnPlaceOrder.TabIndex = 6;
            btnPlaceOrder.Text = "Zatwierdź zamówienie";
            btnPlaceOrder.UseVisualStyleBackColor = false;
            btnPlaceOrder.Click += btnPlaceOrder_Click_1;
            // 
            // btnFulfillOrder
            // 
            btnFulfillOrder.Location = new Point(20, 448);
            btnFulfillOrder.Name = "btnFulfillOrder";
            btnFulfillOrder.Size = new Size(310, 35);
            btnFulfillOrder.TabIndex = 6;
            btnFulfillOrder.Text = "Rozpocznij realizację";
            btnFulfillOrder.UseVisualStyleBackColor = true;
            btnFulfillOrder.Click += btnFulfillOrder_Click_1;
            // 
            // btnOrderReady
            // 
            btnOrderReady.Location = new Point(20, 489);
            btnOrderReady.Name = "btnOrderReady";
            btnOrderReady.Size = new Size(310, 35);
            btnOrderReady.TabIndex = 7;
            btnOrderReady.Text = "Zamówienie gotowe";
            btnOrderReady.UseVisualStyleBackColor = true;
            btnOrderReady.Click += btnOrderReady_Click;
            // 
            // lblExtras
            // 
            lblExtras.AutoSize = true;
            lblExtras.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lblExtras.Location = new Point(198, 40);
            lblExtras.Name = "lblExtras";
            lblExtras.Size = new Size(51, 15);
            lblExtras.TabIndex = 8;
            lblExtras.Text = "Dodatki";
            // 
            // cmbProductSelection
            // 
            cmbProductSelection.Location = new Point(20, 58);
            cmbProductSelection.Name = "cmbProductSelection";
            cmbProductSelection.Size = new Size(150, 23);
            cmbProductSelection.TabIndex = 9;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(20, 87);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(150, 25);
            btnAddProduct.TabIndex = 10;
            btnAddProduct.Text = "Dodaj do zamówienia";
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(20, 40);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 11;
            label1.Text = "Produkt";
            // 
            // listAwaitingOrdersClient
            // 
            listAwaitingOrdersClient.Columns.AddRange(new ColumnHeader[] { NumerZamówienia, Status });
            listAwaitingOrdersClient.FullRowSelect = true;
            listAwaitingOrdersClient.Location = new Point(363, 40);
            listAwaitingOrdersClient.Name = "listAwaitingOrdersClient";
            listAwaitingOrdersClient.Size = new Size(263, 443);
            listAwaitingOrdersClient.TabIndex = 12;
            listAwaitingOrdersClient.UseCompatibleStateImageBehavior = false;
            listAwaitingOrdersClient.View = View.Details;
            // 
            // NumerZamówienia
            // 
            NumerZamówienia.Text = "#";
            NumerZamówienia.Width = 50;
            // 
            // Status
            // 
            Status.Text = "Status";
            Status.Width = 250;
            // 
            // btnPickupOrder
            // 
            btnPickupOrder.Location = new Point(363, 489);
            btnPickupOrder.Name = "btnPickupOrder";
            btnPickupOrder.Size = new Size(263, 35);
            btnPickupOrder.TabIndex = 13;
            btnPickupOrder.Text = "Zamówienie odebrane";
            btnPickupOrder.UseVisualStyleBackColor = true;
            btnPickupOrder.Click += btnPickupOrder_Click;
            // 
            // listAwaitingOrdersBarista
            // 
            listAwaitingOrdersBarista.Columns.AddRange(new ColumnHeader[] { columnHeader1, PozycjeZam });
            listAwaitingOrdersBarista.FullRowSelect = true;
            listAwaitingOrdersBarista.Location = new Point(20, 288);
            listAwaitingOrdersBarista.Name = "listAwaitingOrdersBarista";
            listAwaitingOrdersBarista.Size = new Size(309, 154);
            listAwaitingOrdersBarista.TabIndex = 14;
            listAwaitingOrdersBarista.UseCompatibleStateImageBehavior = false;
            listAwaitingOrdersBarista.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "#";
            columnHeader1.Width = 50;
            // 
            // PozycjeZam
            // 
            PozycjeZam.Text = "Pozycje zamówienia";
            PozycjeZam.Width = 250;
            // 
            // txtStatusLog
            // 
            txtStatusLog.Location = new Point(20, 569);
            txtStatusLog.Multiline = true;
            txtStatusLog.Name = "txtStatusLog";
            txtStatusLog.Size = new Size(606, 120);
            txtStatusLog.TabIndex = 15;
            // 
            // btnDeleteOrder
            // 
            btnDeleteOrder.BackColor = Color.IndianRed;
            btnDeleteOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            btnDeleteOrder.Location = new Point(20, 531);
            btnDeleteOrder.Name = "btnDeleteOrder";
            btnDeleteOrder.Size = new Size(606, 32);
            btnDeleteOrder.TabIndex = 16;
            btnDeleteOrder.Text = "Usuń zamówienie";
            btnDeleteOrder.UseVisualStyleBackColor = false;
            btnDeleteOrder.Click += btnDeleteOrder_Click;
            // 
            // lblBaristaOrders
            // 
            lblBaristaOrders.AutoSize = true;
            lblBaristaOrders.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lblBaristaOrders.Location = new Point(20, 265);
            lblBaristaOrders.Name = "lblBaristaOrders";
            lblBaristaOrders.Size = new Size(202, 21);
            lblBaristaOrders.TabIndex = 17;
            lblBaristaOrders.Text = "Zamówienia do realizacji";
            // 
            // listOrderPositions
            // 
            listOrderPositions.Columns.AddRange(new ColumnHeader[] { PozycjeZamówienia });
            listOrderPositions.FullRowSelect = true;
            listOrderPositions.Location = new Point(21, 119);
            listOrderPositions.Name = "listOrderPositions";
            listOrderPositions.Size = new Size(308, 97);
            listOrderPositions.TabIndex = 18;
            listOrderPositions.UseCompatibleStateImageBehavior = false;
            listOrderPositions.View = View.Details;
            // 
            // PozycjeZamówienia
            // 
            PozycjeZamówienia.Text = "Pozycje Zamówienia";
            PozycjeZamówienia.Width = 300;
            // 
            // btnClearProductsList
            // 
            btnClearProductsList.BackColor = Color.LightCoral;
            btnClearProductsList.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            btnClearProductsList.Location = new Point(228, 225);
            btnClearProductsList.Name = "btnClearProductsList";
            btnClearProductsList.Size = new Size(101, 23);
            btnClearProductsList.TabIndex = 19;
            btnClearProductsList.Text = "Wyczyść listę";
            btnClearProductsList.UseVisualStyleBackColor = false;
            btnClearProductsList.Click += btnClearProductsList_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(641, 682);
            Controls.Add(btnClearProductsList);
            Controls.Add(listOrderPositions);
            Controls.Add(lblBaristaOrders);
            Controls.Add(btnDeleteOrder);
            Controls.Add(txtStatusLog);
            Controls.Add(listAwaitingOrdersBarista);
            Controls.Add(btnPickupOrder);
            Controls.Add(listAwaitingOrdersClient);
            Controls.Add(label1);
            Controls.Add(lblExtras);
            Controls.Add(btnOrderReady);
            Controls.Add(btnPlaceOrder);
            Controls.Add(btnFulfillOrder);
            Controls.Add(lblPendingOrders);
            Controls.Add(lblBaristaView);
            Controls.Add(chkWhipCream);
            Controls.Add(chkMilk);
            Controls.Add(cmbProductSelection);
            Controls.Add(btnAddProduct);
            Name = "MainForm";
            Text = "Kawiarnia - Zarządzanie Zamówieniami";
            ResumeLayout(false);
            PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private Button btnPlaceOrder;
        private Button btnOrderReady;
        private Label lblExtras;
        private ComboBox cmbProductSelection;
        private Button btnAddProduct;
        private Label label1;
        private ListView listAwaitingOrdersClient;
        private ColumnHeader NumerZamówienia;
        private ColumnHeader Status;
        private Button btnPickupOrder;
        private ListView listAwaitingOrdersBarista;
        private ColumnHeader columnHeader1;
        private ColumnHeader PozycjeZam;
        private TextBox txtStatusLog;
        private Button btnDeleteOrder;
        private Label lblBaristaOrders;
        private ListView listOrderPositions;
        private ColumnHeader PozycjeZamówienia;
        private Button btnClearProductsList;
    }
}