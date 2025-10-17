using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Test_Smart_Analytics.DatabaseManager;

namespace Test_Smart_Analytics
{
    public partial class CreateTableForm : Form
    {
        public string TableName => txtTableName.Text.Trim();
        public List<ColumnDefinition> Columns { get; private set; } = new();

        public CreateTableForm()
        {
            InitializeComponent();

            this.dgvColumns.DataError += (s, e) =>
            {
                e.ThrowException = false;

                string columnName = dgvColumns.Columns[e.ColumnIndex].HeaderText;
                MessageBox.Show(
                    this,
                    $"Недопустимое значение в столбце '{columnName}'.\n" +
                    "Выбрано значение по умолчанию.",
                    "Ошибка значения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Можно автоматически установить значение по умолчанию
                if (dgvColumns.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn comboCol && e.RowIndex >= 0)
                {
                    dgvColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = comboCol.Items[0]; // первое значение списка
                }
            };
        }

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

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvColumns.CurrentRow == null || dgvColumns.CurrentRow.Index >= dgvColumns.Rows.Count - 2)
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
