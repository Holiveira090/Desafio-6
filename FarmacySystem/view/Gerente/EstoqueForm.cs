using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FarmacySystem.controller;
using FarmacySystem.model;

namespace FarmacySystem.view
{
    public partial class EstoqueForm : Form
    {
        private MainForm mainForm;
        private CrudMedicine crudMedicine = new CrudMedicine();
        private DataGridView dgvEstoque;
        private TextBox txtId, txtName, txtDescription, txtType, txtPrice;
        private DateTimePicker dtpExpirationDate;
        private Button btnAdd, btnUpdate, btnDelete, btnRefresh, btnVoltar;
        private Panel headerPanel;
        private Label lblHeader;

        public EstoqueForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            LoadData();
            this.Resize += ResizeForm;
        }

        private void InitializeComponent()
        {
            this.Text = "Gerenciamento de Estoque";
            this.Size = new Size(1000, 600);
            this.MinimumSize = new Size(800, 500);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Header
            headerPanel = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(75, 0, 110) };
            lblHeader = new Label
            {
                Text = "Controle de Medicamentos",
                ForeColor = Color.White,
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblHeader);

            // Botão Voltar
            btnVoltar = new Button
            {
                Text = "Voltar",
                BackColor = Color.FromArgb(75, 0, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnVoltar.Click += BtnVoltar_Click;
            headerPanel.Controls.Add(btnVoltar);

            this.Controls.Add(headerPanel);

            // DataGridView
            dgvEstoque = new DataGridView
            {
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvEstoque);

            // Campos de entrada
            txtId = CreateTextBox("ID");
            txtName = CreateTextBox("Nome");
            txtDescription = CreateTextBox("Descrição");
            txtType = CreateTextBox("Tipo");
            txtPrice = CreateTextBox("Preço");
            dtpExpirationDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Arial", 10),
                Width = 150
            };

            // Botões
            btnAdd = CreateButton("Adicionar", Color.FromArgb(100, 180, 100));
            btnUpdate = CreateButton("Atualizar", Color.FromArgb(75, 0, 110));
            btnDelete = CreateButton("Excluir", Color.FromArgb(200, 80, 80));
            btnRefresh = CreateButton("Atualizar", Color.FromArgb(75, 0, 110));

            this.Controls.AddRange(new Control[] {
                txtId, txtName, txtDescription, txtType, txtPrice,
                dtpExpirationDate, btnAdd, btnUpdate, btnDelete, btnRefresh
            });

            // Eventos
            btnAdd.Click += new EventHandler(BtnAdd_Click);
            btnUpdate.Click += new EventHandler(BtnUpdate_Click);
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnRefresh.Click += new EventHandler(BtnRefresh_Click);
            dgvEstoque.SelectionChanged += new EventHandler(DgvEstoque_SelectionChanged);

            ResizeForm(this, EventArgs.Empty);
        }

        private TextBox CreateTextBox(string placeholder)
        {
            return new TextBox
            {
                PlaceholderText = placeholder,
                Font = new Font("Arial", 10),
                Width = 150,
                Margin = new Padding(5)
            };
        }

        private Button CreateButton(string text, Color color)
        {
            var button = new Button
            {
                Text = text,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Cursor = Cursors.Hand
            };
            return button;
        }

        private void ResizeForm(object sender, EventArgs e)
        {
            int margin = 15;
            int headerHeight = headerPanel.Height;

            // Posicionar DataGridView
            dgvEstoque.Location = new Point(margin, headerHeight + margin);
            dgvEstoque.Size = new Size(
                this.ClientSize.Width - 2 * margin,
                this.ClientSize.Height - headerHeight - 150
            );

            // Posicionar campos
            int yPos = dgvEstoque.Bottom + margin;
            int inputWidth = (this.ClientSize.Width - 2 * margin - 5 * 10) / 6;
            txtId.Width = txtName.Width = txtDescription.Width = txtType.Width = txtPrice.Width = dtpExpirationDate.Width = inputWidth;

            txtId.Location = new Point(margin, yPos);
            txtName.Location = new Point(txtId.Right + 10, yPos);
            txtDescription.Location = new Point(txtName.Right + 10, yPos);
            txtType.Location = new Point(txtDescription.Right + 10, yPos);
            txtPrice.Location = new Point(txtType.Right + 10, yPos);
            dtpExpirationDate.Location = new Point(txtPrice.Right + 10, yPos);

            // Posicionar botões
            int buttonY = yPos + 50;
            int buttonWidth = (this.ClientSize.Width - 5 * margin) / 4;
            btnAdd.Size = btnUpdate.Size = btnDelete.Size = btnRefresh.Size = new Size(buttonWidth, 35);
            btnAdd.Location = new Point(margin, buttonY);
            btnUpdate.Location = new Point(btnAdd.Right + margin, buttonY);
            btnDelete.Location = new Point(btnUpdate.Right + margin, buttonY);
            btnRefresh.Location = new Point(btnDelete.Right + margin, buttonY);

            // Posicionar botão Voltar
            btnVoltar.Location = new Point(
                headerPanel.Width - btnVoltar.Width - margin, // Alinhar à direita com margem
                (headerPanel.Height - btnVoltar.Height) / 2 // Centralizar verticalmente
            );
        }

        private void LoadData()
        {
            try
            {
                var medicines = crudMedicine.ListMedicines();
                dgvEstoque.DataSource = medicines.Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.Description,
                    m.Type,
                    Preço = m.Price.ToString("C2"),
                    Validade = m.ExpirationDate?.ToString("dd/MM/yyyy") ?? "N/A"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            try
            {
                decimal price = decimal.Parse(txtPrice.Text, System.Globalization.NumberStyles.Currency);

                // Converta o DateTime para UTC antes de enviar ao banco de dados
                DateTime expirationDate = DateTime.SpecifyKind(dtpExpirationDate.Value, DateTimeKind.Utc);

                crudMedicine.InsertMedicine(
                    0,
                    nameM: txtName.Text,
                    descriptionM: txtDescription.Text,
                    typeM: txtType.Text,
                    priceM: price,
                    expiration_dateM: expirationDate // Use o DateTime convertido
                );

                LoadData();
                ClearFields();
                MessageBox.Show("Medicamento cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um medicamento para atualizar!");
                return;
            }

            if (!ValidateFields()) return;

            try
            {
                decimal price = decimal.Parse(txtPrice.Text, System.Globalization.NumberStyles.Currency);
                DateTime expirationDate = DateTime.SpecifyKind(dtpExpirationDate.Value, DateTimeKind.Utc);

                crudMedicine.MedicinesUpdate(
                    int.Parse(txtId.Text),
                    txtName.Text,
                    txtDescription.Text,
                    txtType.Text,
                    price,
                    expirationDate
                );
                LoadData();
                MessageBox.Show("Medicamento atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um medicamento para excluir!");
                return;
            }

            if (MessageBox.Show("Tem certeza que deseja excluir?", "Confirmação",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    crudMedicine.MedicinesDelete(int.Parse(txtId.Text));
                    LoadData();
                    ClearFields();
                    MessageBox.Show("Medicamento excluído com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir: {ex.Message}");
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearFields();
        }

        private void DgvEstoque_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEstoque.CurrentRow != null)
            {
                var row = dgvEstoque.CurrentRow;
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtType.Text = row.Cells["Type"].Value.ToString();
                txtPrice.Text = row.Cells["Preço"].Value.ToString().Replace("R$", "").Trim();
                dtpExpirationDate.Value = DateTime.Parse(row.Cells["Validade"].Value.ToString());
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("O campo Descrição é obrigatório!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("O campo Tipo é obrigatório!");
                return false;
            }
            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Preço inválido!");
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtType.Clear();
            txtPrice.Clear();
            dtpExpirationDate.Value = DateTime.Now;
        }
        
        private void BtnVoltar_Click(object sender, EventArgs e)
        {
           mainForm.TrocarTela(new ManagerForm(mainForm));
        }
    }
}