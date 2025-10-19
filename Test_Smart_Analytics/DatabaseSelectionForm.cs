using System;
using System.Windows.Forms;

namespace Test_Smart_Analytics
{
    public partial class DatabaseSelectionForm : Form
    {
        public string Host => txtHost.Text.Trim();
        public string Port => txtPort.Text.Trim();
        public string DatabaseName => txtDatabaseName.Text.Trim();
        public string Username => txtUsername.Text.Trim();
        public string Password => txtPassword.Text.Trim();

        private TextBox txtHost;
        private TextBox txtPort;
        private TextBox txtDatabaseName;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnOk;
        private Button btnCancel;

        public DatabaseSelectionForm()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            Text = "Подключение к базе данных";
            Width = 420;
            Height = 380;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label lblHeader = new Label
            {
                Text = "Введите параметры подключения:",
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            TableLayoutPanel inputPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 2,
                Padding = new Padding(20),
                AutoSize = true
            };
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            txtHost = new TextBox { PlaceholderText = "Например: localhost", BackColor = Color.LightGray };
            txtPort = new TextBox { PlaceholderText = "5432", BackColor = Color.LightGray, Text = "5432" };
            txtDatabaseName = new TextBox { PlaceholderText = "Имя базы данных", BackColor = Color.LightGray };
            txtUsername = new TextBox { PlaceholderText = "Имя пользователя (например: postgres)", BackColor = Color.LightGray, Text = "postgres" };
            txtPassword = new TextBox { PlaceholderText = "Пароль", BackColor = Color.LightGray, UseSystemPasswordChar = true };

            inputPanel.Controls.Add(new Label { Text = "Хост:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 0);
            inputPanel.Controls.Add(txtHost, 1, 0);
            inputPanel.Controls.Add(new Label { Text = "Порт:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 1);
            inputPanel.Controls.Add(txtPort, 1, 1);
            inputPanel.Controls.Add(new Label { Text = "База данных:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 2);
            inputPanel.Controls.Add(txtDatabaseName, 1, 2);
            inputPanel.Controls.Add(new Label { Text = "Пользователь:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 3);
            inputPanel.Controls.Add(txtUsername, 1, 3);
            inputPanel.Controls.Add(new Label { Text = "Пароль:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 4);
            inputPanel.Controls.Add(txtPassword, 1, 4);

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 50,
                Padding = new Padding(10)
            };

            btnOk = new Button { Text = "Подключиться", Width = 120, Height = 35 };
            btnCancel = new Button { Text = "Отмена", Width = 100, Height = 35 };
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            buttonsPanel.Controls.Add(btnOk);
            buttonsPanel.Controls.Add(btnCancel);

            Controls.Add(inputPanel);
            Controls.Add(lblHeader);
            Controls.Add(buttonsPanel);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                MessageBox.Show("Введите имя базы данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(Host))
            {
                MessageBox.Show("Введите хост!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Введите имя пользователя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
