using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Test_Smart_Analytics.DatabaseManager;

namespace Test_Smart_Analytics
{
    public partial class CreateTableForm : Form
    {
        public DataGridView dgvColumns => _dgvColumns;
        public TextBox txtTableName => _txtTableName;

        public string TableName => txtTableName.Text.Trim();
        public List<ColumnDefinition> Columns { get; private set; } = new();

        private TextBox _txtTableName;
        private DataGridView _dgvColumns;
        private Button btnOk;
        private Button btnCancel;
        private Button btnMoveUp;
        private Button btnMoveDown;

        public CreateTableForm()
        {
            InitializeComponent();
            InitUI();

            // обработка ошибок DataGridViewComboBoxCell
            _dgvColumns.DataError += DgvColumns_DataError;
        }

        private void DgvColumns_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;

            string columnName = _dgvColumns.Columns[e.ColumnIndex].HeaderText;
            string fieldName = _dgvColumns.Rows[e.RowIndex].Cells["Name"].Value?.ToString() ?? "(без имени)";
            MessageBox.Show(
                this,
                $"Недопустимое значение в столбце \"{columnName}\" для поля \"{fieldName}\".\n" +
                $"Установлено значение по умолчанию.",
                "Ошибка значения",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            // Если это ComboBoxColumn — ставим первый допустимый элемент
            if (_dgvColumns.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn comboCol &&
                comboCol.Items.Count > 0 && e.RowIndex >= 0)
            {
                _dgvColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = comboCol.Items[0];
            }
        }

        private void InitUI()
        {
            Text = "Создание таблицы";
            Width = 700;
            Height = 450;

            Label lbl = new Label { Text = "Имя таблицы:", Dock = DockStyle.Top, Height = 25 };
            _txtTableName = new TextBox { Dock = DockStyle.Top, Height = 25 };

            _dgvColumns = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvColumns.Columns.Add("Name", "Имя поля");
            dgvColumns.Columns.Add(new DataGridViewComboBoxColumn()
            {
                HeaderText = "Тип данных",
                Name = "Type",
                DataSource = new List<string> { "integer", "bigint", "double precision", "text", "timestamp" }
            });
            dgvColumns.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "Первичный ключ",
                Name = "IsPrimaryKey"
            });
            dgvColumns.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "NOT NULL",
                Name = "NotNull"
            });

            // Кнопки управления
            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                FlowDirection = FlowDirection.RightToLeft
            };

            btnOk = new Button { Text = "Создать", Width = 100 };
            btnCancel = new Button { Text = "Отмена", Width = 100 };
            btnMoveUp = new Button { Text = "⬆️ Вверх", Width = 100 };
            btnMoveDown = new Button { Text = "⬇️ Вниз", Width = 100 };

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
            btnMoveUp.Click += BtnMoveUp_Click;
            btnMoveDown.Click += BtnMoveDown_Click;

            buttonsPanel.Controls.Add(btnCancel);
            buttonsPanel.Controls.Add(btnOk);
            buttonsPanel.Controls.Add(btnMoveDown);
            buttonsPanel.Controls.Add(btnMoveUp);

            Controls.Add(dgvColumns);
            Controls.Add(txtTableName);
            Controls.Add(lbl);
            Controls.Add(buttonsPanel);
        }

        // === Перемещение строк вверх ===
        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvColumns.CurrentRow == null || dgvColumns.CurrentRow.Index == 0)
                return;

            int index = dgvColumns.CurrentRow.Index;
            var row = dgvColumns.Rows[index];
            dgvColumns.Rows.RemoveAt(index);
            dgvColumns.Rows.Insert(index - 1, row);
            dgvColumns.CurrentCell = dgvColumns.Rows[index - 1].Cells[0];
        }

        // === Перемещение строк вниз ===
        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvColumns.CurrentRow == null || dgvColumns.CurrentRow.Index == dgvColumns.Rows.Count - 2)
                return;

            int index = dgvColumns.CurrentRow.Index;
            var row = dgvColumns.Rows[index];
            dgvColumns.Rows.RemoveAt(index);
            dgvColumns.Rows.Insert(index + 1, row);
            dgvColumns.CurrentCell = dgvColumns.Rows[index + 1].Cells[0];
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TableName))
            {
                MessageBox.Show("Введите имя таблицы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Columns.Clear();

            foreach (DataGridViewRow row in dgvColumns.Rows)
            {
                if (row.IsNewRow) continue;

                string name = row.Cells["Name"].Value?.ToString()?.Trim() ?? "";
                string type = row.Cells["Type"].Value?.ToString() ?? "";
                bool isPk = row.Cells["IsPrimaryKey"].Value is bool pk && pk;
                bool notNull = row.Cells["NotNull"].Value is bool nn && nn;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
                    continue;

                Columns.Add(new ColumnDefinition
                {
                    Name = name,
                    Type = type,
                    IsPrimaryKey = isPk,
                    NotNull = notNull
                });
            }

            if (Columns.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одно поле!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
