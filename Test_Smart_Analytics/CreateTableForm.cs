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
            StartPosition = FormStartPosition.CenterParent;

            Label lblTableName = new Label
            {
                Text = "Имя таблицы:",
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            _txtTableName = new TextBox
            {
                Dock = DockStyle.Top,
                Height = 25,
                BackColor = Color.LightGray
            };
            SetPlaceholder(_txtTableName, "Введите имя таблицы...");

            _dgvColumns = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false
            };
            _dgvColumns.CellValidating += DgvColumns_CellValidating;
            _dgvColumns.CellValueChanged += DgvColumns_CellValueChanged;
            _dgvColumns.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (_dgvColumns.IsCurrentCellDirty)
                    _dgvColumns.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            _dgvColumns.Columns.Add("Name", "Имя поля");
            _dgvColumns.Columns.Add(new DataGridViewComboBoxColumn
            {
                HeaderText = "Тип данных",
                Name = "Type",
                DataSource = new List<string> { "integer", "bigint", "double precision", "text", "timestamp" }
            });
            _dgvColumns.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Первичный ключ",
                Name = "IsPrimaryKey"
            });
            _dgvColumns.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "NOT NULL",
                Name = "NotNull"
            });

            _dgvColumns.DataError += (s, e) =>
            {
                e.ThrowException = false;
                MessageBox.Show(
                    $"Недопустимое значение в поле: {_dgvColumns.CurrentRow?.Cells["Name"].Value}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            };

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(5)
            };

            btnOk = new Button { Text = "Сохранить", Width = 100, Height = 35 };
            AcceptButton = btnOk;
            btnCancel = new Button { Text = "Отмена", Width = 100, Height = 35 };
            CancelButton = btnCancel;
            btnMoveUp = new Button { Text = "Вверх", Width = 100, Height = 35 };
            btnMoveDown = new Button { Text = "Вниз", Width = 100, Height = 35 };

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
            btnMoveUp.Click += BtnMoveUp_Click;
            btnMoveDown.Click += BtnMoveDown_Click;

            buttonsPanel.Controls.AddRange(new Control[] { btnCancel, btnOk, btnMoveDown, btnMoveUp });

            Controls.Add(_dgvColumns);
            Controls.Add(_txtTableName);
            Controls.Add(lblTableName);
            Controls.Add(buttonsPanel);
        }

        private void DgvColumns_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name == "IsPrimaryKey")
            {
                var isPkCell = grid.Rows[e.RowIndex].Cells["IsPrimaryKey"];
                var notNullCell = grid.Rows[e.RowIndex].Cells["NotNull"];

                bool isPk = isPkCell.Value is bool value && value;

                if (isPk)
                {
                    notNullCell.Value = true;
                    notNullCell.ReadOnly = true;
                    notNullCell.Style.BackColor = Color.LightGray;
                }
                else
                {
                    notNullCell.ReadOnly = false;
                    notNullCell.Style.BackColor = Color.White;
                }
            }
        }


        private void DgvColumns_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_dgvColumns.Columns[e.ColumnIndex].Name == "Name")
            {
                string input = e.FormattedValue?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (input.Contains(" ") || !System.Text.RegularExpressions.Regex.IsMatch(input, @"^[A-Za-z_][A-Za-z0-9_]*$"))
                {
                    MessageBox.Show(
                        "Недопустимое имя поля!\n" +
                        "Имя поля может содержать только буквы, цифры и символ подчёркивания, " +
                        "и не может начинаться с цифры или содержать пробелы.",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    e.Cancel = true;
                }
            }
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
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
