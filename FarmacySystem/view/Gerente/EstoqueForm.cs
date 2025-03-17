using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmacySystem.view
{
    public class EstoqueForm : Form
    {
        private Panel headerPanel = null!;
        private Label lblHeader = null!;
        private DataGridView dgvEstoque = null!;
        private TextBox txtQuantity = null!;
        private DateTimePicker dtpUpdatedAt = null!;
        private TextBox txtMedicineId = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnRefresh = null!;

        public EstoqueForm()
        {
            InitializeComponent();
            this.Resize += new EventHandler(ResizeForm);
        }

        private void InitializeComponent()
        {
            // Configurações do Form
            this.Text = "DigiMed Pharmacy - Estoque";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.BackColor = Color.FromArgb(255, 245, 245);

            // Painel de Cabeçalho
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(75, 0, 110)
            };
            this.Controls.Add(headerPanel);

            // Label do Cabeçalho
            lblHeader = new Label
            {
                Text = "Gerenciamento de Estoque",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblHeader);

            // Tabela de Estoque
            dgvEstoque = new DataGridView
            {
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Size = new Size(600, 300)
            };
            this.Controls.Add(dgvEstoque);

            // Caixa de texto "Quantidade"
            txtQuantity = new TextBox
            {
                PlaceholderText = "Quantidade",
                Size = new Size(150, 30),
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            this.Controls.Add(txtQuantity);

            // Caixa de Data "Atualizado em"
            dtpUpdatedAt = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Size = new Size(200, 30),
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            this.Controls.Add(dtpUpdatedAt);

            // Caixa de texto "ID do Medicamento"
            txtMedicineId = new TextBox
            {
                PlaceholderText = "ID do Medicamento",
                Size = new Size(150, 30),
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            this.Controls.Add(txtMedicineId);

            // Botões
            btnAdd = CriarBotao("Adicionar", BtnAdd_Click);
            btnUpdate = CriarBotao("Atualizar", BtnUpdate_Click);
            btnDelete = CriarBotao("Excluir", BtnDelete_Click);
            btnRefresh = CriarBotao("Atualizar Lista", BtnRefresh_Click);

            // Adiciona componentes ao formulário
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnRefresh);

            // Posicionamento inicial
            ResizeForm(null, null);
        }

        private Button CriarBotao(string texto, EventHandler evento)
        {
            var button = new Button
            {
                Text = texto,
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(75, 0, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            button.Click += evento;
            return button;
        }

        private void ResizeForm(object? sender, EventArgs? e)
        {
            int centerX = this.ClientSize.Width / 2;
            int startY = 80;

            lblHeader.Location = new Point((headerPanel.Width - lblHeader.Width) / 2, 15);
            dgvEstoque.Location = new Point(centerX - dgvEstoque.Width / 2, startY);
            txtQuantity.Location = new Point(centerX - 300, startY + 320);
            dtpUpdatedAt.Location = new Point(centerX - 150, startY + 320);
            txtMedicineId.Location = new Point(centerX + 50, startY + 320);
            btnAdd.Location = new Point(centerX - 250, startY + 370);
            btnUpdate.Location = new Point(centerX - 90, startY + 370);
            btnDelete.Location = new Point(centerX + 70, startY + 370);
            btnRefresh.Location = new Point(centerX + 230, startY + 370);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Adicionar item ao estoque");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Atualizar item do estoque");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Excluir item do estoque");
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Atualizar lista de estoque");
        }
    }
}
