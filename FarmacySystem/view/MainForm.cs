using System;
using System.Drawing;
using System.Windows.Forms;
using FarmacySystem.controller;

namespace FarmacySystem.view
{
    public class MainForm : Form
    {
        private Panel panelContainer;

        public MainForm()
        {
            InitializeComponent();
            ConfigurarPanelContainer();
            MostrarTelaLogin();
        }

        private void InitializeComponent()
        {
            // Configurações do Form
            this.Text = "DigiMed Pharmacy";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 500); // Evita redimensionamento muito pequeno
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true; 
            this.BackColor = Color.FromArgb(255, 245, 245); 
        }
        private void ConfigurarPanelContainer()
        {
            panelContainer = new Panel
            {
                Dock = DockStyle.Fill,  // Ocupa todo o espaço do formulário
                BorderStyle = BorderStyle.FixedSingle // Apenas para visualização
            };
            this.Controls.Add(panelContainer);
        }


        public void TrocarTela(Form novaTela)
        {
            panelContainer.Controls.Clear();
            
            novaTela.TopLevel = false;  // Faz o Form se comportar como um controle dentro do MainForm
            novaTela.Dock = DockStyle.Fill;
            novaTela.FormBorderStyle = FormBorderStyle.None;
            panelContainer.Controls.Add(novaTela);
            novaTela.Show();
        }

        private void MostrarTelaLogin()
        {
            TrocarTela(new LoginForm(this));  // Passamos o próprio MainForm para o LoginForm
        }
    }
}