using System;
using System.Windows.Forms;

namespace Test_Smart_Analytics
{
    public partial class DatabaseSelectionForm : Form
    {
        public string DatabaseName => txtDatabaseName.Text.Trim();

        private TextBox txtDatabaseName;
        private Button btnOk;
        private Button btnCancel;

        public DatabaseSelectionForm()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            Text = "Выбор базы данных";
            Width = 400;
            Height = 180;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label lbl = new Label
            {
                Text = "Введите имя базы данных для подключения:",
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            txtDatabaseName = new TextBox
            {
                Dock = DockStyle.Top,
                Height = 30,
                Margin = new Padding(10),
                PlaceholderText = "Например: Test"
            };

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 50
            };

            btnOk = new Button { Text = "Подключиться", Width = 120, Height = 35 };
            AcceptButton = btnOk;
            btnCancel = new Button { Text = "Отмена", Width = 100, Height = 35 };
            CancelButton = btnCancel;

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            buttonsPanel.Controls.Add(btnOk);
            buttonsPanel.Controls.Add(btnCancel);

            Controls.Add(buttonsPanel);
            Controls.Add(txtDatabaseName);
            Controls.Add(lbl);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                MessageBox.Show("Введите имя базы данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
