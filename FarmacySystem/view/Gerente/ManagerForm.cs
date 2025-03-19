using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmacySystem.view
{
    public partial class ManagerForm : Form
    {
        private Button btnUser, btnMedicine, btnSupplier, btnSales;
        private Label lblHeader, lblLogout;
        private TableLayoutPanel tableLayoutPanel;
        private Panel panelHeader;
        private MainForm mainForm;
        public ManagerForm(MainForm form)
        {
            mainForm = form;
            InitializeComponent();
            ConfigurarTela();
        }

        private void InitializeComponent()
        {
            this.Text = "Gerente";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 500);
        }
        private void ConfigurarTela()
        {
            tableLayoutPanel = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = false,  // Evita alterações inesperadas no tamanho
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,  // Remove bordas
                BackColor = Color.FromArgb(180, 180, 251),
                Margin = new Padding(0),
                Padding = new Padding(0),
            };

            panelHeader = new Panel
            {
                Height = 60,
                BackColor = Color.FromArgb(75, 0, 110),
                Dock = DockStyle.Top,  // Fixa o painel no topo sem expandir
                Padding = new Padding(0),
                Margin = new Padding(0),
            };

            lblHeader = new Label
            {
                Text = "Gerente",
                AutoSize = true,
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Padding = new Padding(10),
            };

            lblLogout = new Label
            {
                Text = "X",
                AutoSize = true,
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Right,
                Padding = new Padding(10),
            };

            panelHeader.Controls.Add(lblHeader);
            panelHeader.Controls.Add(lblLogout);

            // Configuração do layout das colunas e linhas
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Header
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

            btnUser = CreateButton("Usuários");
            btnMedicine = CreateButton("Medicamentos");
            btnSupplier = CreateButton("Fornecedores");
            btnSales = CreateButton("Vendas");

            tableLayoutPanel.Controls.Add(panelHeader, 0, 0);
            tableLayoutPanel.SetColumnSpan(panelHeader, 2);

            tableLayoutPanel.Controls.Add(btnUser, 0, 2);
            tableLayoutPanel.Controls.Add(btnSupplier, 1, 2);
            tableLayoutPanel.Controls.Add(btnMedicine, 0, 3);
            tableLayoutPanel.Controls.Add(btnSales, 1, 3);

            Controls.Add(tableLayoutPanel);

            lblLogout.Click += new EventHandler(LblLogout_Click);
            btnUser.Click += new EventHandler(BtnUser_Click);
            btnMedicine.Click += new EventHandler(BtnMedicine_Click);
            btnSales.Click += new EventHandler(BtnSales_Click);
            btnSupplier.Click += new EventHandler(BtnSupplier_Click);
        }

        private Button CreateButton(string text)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(100, 30),
                Font = new Font("Segoi UI", 12, FontStyle.Bold),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.None,  // Não deixa o botão expandir
                Margin = new Padding(10), // Dá um espaçamento interno uniforme
                Width = 300,
                Height = 80,
                AutoSize = false, // Desativa o auto tamanho
            };

            return button;
        }

        private void LblLogout_Click(object sender, EventArgs e)
        {
            mainForm.TrocarTela(new LoginForm(mainForm));
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            mainForm.TrocarTela(new FormCadastro(mainForm));
        }
        private void BtnMedicine_Click(object sender, EventArgs e)
        {
            mainForm.TrocarTela(new FormCadastro(mainForm));
        }
        private void BtnSales_Click(object sender, EventArgs e)
        {
            mainForm.TrocarTela(new ReportListForm(mainForm));
        }
        private void BtnSupplier_Click(object sender, EventArgs e)
        {
            mainForm.TrocarTela(new FormCadastro(mainForm));
    }    }    } 