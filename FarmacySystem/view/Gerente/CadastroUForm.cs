using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmacySystem
{
    public partial class FormCadastro : Form
    {
        private Panel headerPanel = null!;
        private Label lblHeader = null!;
        private Label lblNome = null!;
        private TextBox txtNome = null!;
        private Label lblCargo = null!;
        private TextBox txtCargo = null!;
        private Label lblLogin = null!;
        private TextBox txtLogin = null!;
        private Label lblSenha = null!;
        private TextBox txtSenha = null!;
        private Button btnVoltar = null!;
        private Button btnEnviar = null!;

        public FormCadastro()
        {
            InitializeComponent();
            this.Resize += new EventHandler(ResizeForm);
        }

        private void InitializeComponent()
        {
            this.Text = "Cadastro - DigiMed Pharmacy";
            this.Size = new Size(600, 700);
            this.MinimumSize = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(180, 180, 251);

            // Painel de cabeçalho
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(75, 0, 110)
            };
            this.Controls.Add(headerPanel);

            lblHeader = new Label
            {
                Text = "DigiMed Pharmacy - Cadastro",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblHeader);

            lblNome = CreateLabel("Nome");
            txtNome = CreateTextBox();
            lblCargo = CreateLabel("Cargo");
            txtCargo = CreateTextBox();
            lblLogin = CreateLabel("Login (CPF)");
            txtLogin = CreateTextBox();
            lblSenha = CreateLabel("Senha");
            txtSenha = CreateTextBox();
            txtSenha.PasswordChar = '*';

            btnVoltar = CreateButton("Voltar", Color.FromArgb(255, 102, 102), (s, e) => this.Close());
            btnEnviar = CreateButton("Enviar", Color.FromArgb(75, 0, 110), (s, e) =>
            {
                MessageBox.Show("Cadastro efetuado com sucesso!", "Sucesso");
                this.Close();
            });

            this.Controls.AddRange(new Control[] { lblNome, txtNome, lblCargo, txtCargo, lblLogin, txtLogin, lblSenha, txtSenha, btnVoltar, btnEnviar });
            ResizeForm(null, null);
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true
            };
        }

        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                Size = new Size(250, 30),
                BackColor = Color.FromArgb(255, 255, 255),
                Font = new Font("Segoe UI", 12F, FontStyle.Regular)
            };
        }

        private Button CreateButton(string text, Color color, EventHandler onClick)
        {
            return new Button
            {
                Text = text,
                Size = new Size(120, 40),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void ResizeForm(object? sender, EventArgs? e)
        {
            int centerX = this.ClientSize.Width / 2;
            int startY = this.ClientSize.Height / 5;
            int spacing = 50;

            lblHeader.Location = new Point((headerPanel.Width - lblHeader.Width) / 2, 15);
            lblNome.Location = new Point(centerX - lblNome.Width / 2, startY);
            txtNome.Location = new Point(centerX - txtNome.Width / 2, startY + 25);
            lblCargo.Location = new Point(centerX - lblCargo.Width / 2, startY + spacing * 2);
            txtCargo.Location = new Point(centerX - txtCargo.Width / 2, startY + spacing * 2 + 25);
            lblLogin.Location = new Point(centerX - lblLogin.Width / 2, startY + spacing * 4);
            txtLogin.Location = new Point(centerX - txtLogin.Width / 2, startY + spacing * 4 + 25);
            lblSenha.Location = new Point(centerX - lblSenha.Width / 2, startY + spacing * 6);
            txtSenha.Location = new Point(centerX - txtSenha.Width / 2, startY + spacing * 6 + 25);
            btnVoltar.Location = new Point(centerX - btnVoltar.Width - 10, startY + spacing * 8);
            btnEnviar.Location = new Point(centerX + 10, startY + spacing * 8);
        }
    }
}
