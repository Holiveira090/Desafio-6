using System;
using System.Drawing;
using System.Windows.Forms;
using FarmacySystem.controller;
using FarmacySystem.view.Farmaceutico;

namespace FarmacySystem.view
{
    public class LoginForm : Form
    {
        private MainForm mainForm;
        private Panel headerPanel = null!;
        private Label lblHeader = null!;
        private Label lblLogin = null!;
        private TextBox txtLogin = null!;
        private Label lblSenha = null!;
        private TextBox txtSenha = null!;
        private Button btnEnviar = null!;
        private CrudUser crudUser;

        public LoginForm(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
            this.Resize += new EventHandler(ResizeForm); 
            crudUser = new CrudUser();
            btnEnviar.Click += new EventHandler(BtnEnviar_Click);
        }

        private void InitializeComponent()
        {
            // Configurações do Form
            this.Text = "DigiMed Pharmacy - Login";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 500); // Evita redimensionamento muito pequeno
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true; 
            this.BackColor = Color.FromArgb(255, 245, 245); 

            // Painel de Cabeçalho (roxo)
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
                Text = "DigiMed Pharmacy",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblHeader);

            // Label "Login"
            lblLogin = new Label
            {
                Text = "Login",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                AutoSize = true
            };
            this.Controls.Add(lblLogin);

            // Caixa de texto "Login"
            txtLogin = new TextBox
            {
                Name = "txtLogin",
                Size = new Size(400, 55),
                BackColor = Color.FromArgb(255, 186, 150),
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            this.Controls.Add(txtLogin);

            // Label "Senha"
            lblSenha = new Label
            {
                Text = "Senha",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                AutoSize = true
            };
            this.Controls.Add(lblSenha);

            // Caixa de texto "Senha"
            txtSenha = new TextBox
            {
                Name = "txtSenha",
                Size = new Size(400, 55),
                PasswordChar = '*',
                BackColor = Color.FromArgb(255, 186, 150),
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            this.Controls.Add(txtSenha);

            // Botão "Enviar"
            btnEnviar = new Button
            {
                Text = "Enviar",
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(75, 0, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            this.Controls.Add(btnEnviar);

            // Chama a função de posicionamento inicial
            ResizeForm(null, null);
        }

        private void ResizeForm(object? sender, EventArgs? e)
        {
            int centerX = this.ClientSize.Width / 2;
            int startY = this.ClientSize.Height / 3;

            // Centraliza os elementos horizontalmente
            lblHeader.Location = new Point((headerPanel.Width - lblHeader.Width) / 2, 15);
            lblLogin.Location = new Point(centerX - lblLogin.Width / 2, startY);
            txtLogin.Location = new Point(centerX - txtLogin.Width / 2, startY + 30);
            lblSenha.Location = new Point(centerX - lblSenha.Width / 2, startY + 80);
            txtSenha.Location = new Point(centerX - txtSenha.Width / 2, startY + 110);
            btnEnviar.Location = new Point(centerX - btnEnviar.Width / 2, startY + 170);
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            try 
            {

                (bool success, string role) = crudUser.LoginUser(txtLogin.Text, txtSenha.Text);

                if (success == true)
                {
                    Form telaUsuario = role switch
                {
                    "gerente" => new ManagerForm(),
                    "farmaceutico" => new FarmaceuticoForm(),
                    _ => new SalesmanForm()
                };

                mainForm.TrocarTela(telaUsuario);
                    
                } else
                {
                    MessageBox.Show("Erro ao realizar login", role);
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Erro no forms", ex.Message);
            }
        }
    }
}
