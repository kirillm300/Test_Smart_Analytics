using System.Data;
using System.Windows.Forms;

namespace Test_Smart_Analytics
{
    public partial class MainForm : Form
    {
        private DatabaseManager? dbManager;
        private readonly string _databaseName;
        private ToolStripMenuItem editTableItem;
        private ToolStripMenuItem deleteTableItem;

        public MainForm(string databaseName)
        {
            _databaseName = databaseName;

            InitializeComponent();
            SetupLabels();
            SetupMenu();
        }

        private void SetupMenu()
        {
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem tableItem = new ToolStripMenuItem("Таблица");

            ToolStripMenuItem createTableItem = new ToolStripMenuItem("Создать таблицу");
            createTableItem.Click += createToolStripMenuItem_Click;

            editTableItem = new ToolStripMenuItem("Редактировать таблицу");
            editTableItem.Enabled = false;
            editTableItem.Click += editToolStripMenuItem_Click;

            deleteTableItem = new ToolStripMenuItem("Удалить таблицу");
            deleteTableItem.Enabled = false;
            deleteTableItem.Click += deleteToolStripMenuItem_Click;

            tableItem.DropDownItems.Add(createTableItem);
            tableItem.DropDownItems.Add(editTableItem);
            tableItem.DropDownItems.Add(deleteTableItem);
            menu.Items.Add(tableItem);

            ToolStripMenuItem helpItem = new ToolStripMenuItem("Помощь");

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе");
            aboutItem.Click += (s, e) =>
            {
                MessageBox.Show(
                    "Это приложение для редактирования таблиц в базе данных — тестовое задание для Smart Analytics.\n\nВерсия 1.0",
                    "О программе",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            ToolStripMenuItem guideItem = new ToolStripMenuItem("Руководство пользователя");
            guideItem.Click += (s, e) =>
            {
                MessageBox.Show(
                    "1. При запуске введите имя базы данных для подключения.\n" +
                    "2. В левой части отображается список таблиц выбранной БД.\n" +
                    "3. Чтобы создать таблицу, выберите пункт меню «Таблица ? Создать таблицу».\n" +
                    "4. Чтобы редактировать или удалить таблицу, выберите её в списке слева.\n" +
                    "5. Действия также можно выполнять через контекстное меню по правому клику.\n" +
                    "6. При ошибках приложение покажет диалог с подробностями.",
                    "Руководство пользователя",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            helpItem.DropDownItems.Add(aboutItem);
            helpItem.DropDownItems.Add(guideItem);
            menu.Items.Add(helpItem);

            menu.Dock = DockStyle.Top;
            this.MainMenuStrip = menu;
            this.Controls.Add(menu);
        }



        private void SetupLabels()
        {
            int labelHeight = 25;
            int spacing = 5;

            Label lblTables = new Label
            {
                Text = "Список таблиц",
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = SystemColors.ControlLight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top
            };
            splitContainerMain.Panel1.Controls.Add(lblTables);

            listBoxTables.Location = new Point(0, lblTables.Height + spacing);
            listBoxTables.Width = splitContainerMain.Panel1.ClientSize.Width;
            listBoxTables.Height = splitContainerMain.Panel1.ClientSize.Height - lblTables.Height - spacing;
            listBoxTables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            splitContainerMain.Panel1.Controls.Add(listBoxTables);


            Label lblStructure = new Label
            {
                Text = "Структура таблицы",
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = SystemColors.ControlLight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top
            };
            splitContainerMain.Panel2.Controls.Add(lblStructure);

            dataGridViewStructure.Location = new Point(0, lblStructure.Height + spacing);
            dataGridViewStructure.Width = splitContainerMain.Panel2.ClientSize.Width;
            dataGridViewStructure.Height = splitContainerMain.Panel2.ClientSize.Height - lblStructure.Height - spacing;
            dataGridViewStructure.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            splitContainerMain.Panel2.Controls.Add(dataGridViewStructure);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string connString = $"Host=localhost;Port=5432;Database={_databaseName};Username=postgres;Password=145311";
                dbManager = new DatabaseManager(connString);
                dbManager.Connect();

                var tables = dbManager.GetUserTables();
                listBoxTables.Items.Clear();
                listBoxTables.Items.AddRange(tables.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к БД '{_databaseName}'",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                Application.Exit();
            }
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dbManager == null)
                return;

            bool hasSelection = listBoxTables.SelectedItem != null;
            editTableItem.Enabled = hasSelection;
            deleteTableItem.Enabled = hasSelection;

            if (!hasSelection)
            {
                dataGridViewStructure.DataSource = null;
                return;
            }

            string tableName = listBoxTables.SelectedItem.ToString()!;
            try
            {
                DataTable structure = dbManager.GetTableStructure(tableName);
                dataGridViewStructure.DataSource = structure;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке структуры таблицы:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dbManager == null) return;

            using var form = new CreateTableForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dbManager.CreateTable(form.TableName, form.Columns);
                    MessageBox.Show("Таблица успешно создана!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var tables = dbManager.GetUserTables();
                    listBoxTables.Items.Clear();
                    listBoxTables.Items.AddRange(tables.ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании таблицы:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem == null)
            {
                MessageBox.Show("Выберите таблицу для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tableName = listBoxTables.SelectedItem.ToString();

            try
            {
                var columns = dbManager.GetTableColumns(tableName);

                using var form = new CreateTableForm();
                form.Text = $"Редактирование таблицы: {tableName}";

                foreach (var col in columns)
                {
                    int rowIndex = form.dgvColumns.Rows.Add();
                    var row = form.dgvColumns.Rows[rowIndex];
                    row.Cells["Name"].Value = col.Name;
                    row.Cells["Type"].Value = col.Type;
                    row.Cells["IsPrimaryKey"].Value = col.IsPrimaryKey;
                    row.Cells["NotNull"].Value = col.NotNull;
                }
                form.txtTableName.Text = tableName;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dbManager.RecreateTable(tableName, form.TableName, form.Columns);
                    MessageBox.Show("Изменения успешно применены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var tables = dbManager.GetUserTables();
                    listBoxTables.Items.Clear();
                    listBoxTables.Items.AddRange(tables.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании таблицы:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem == null)
            {
                MessageBox.Show("Выберите таблицу для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tableName = listBoxTables.SelectedItem.ToString();

            var confirm = MessageBox.Show(
                $"Вы действительно хотите удалить таблицу \"{tableName}\"?\nВсе данные будут потеряны.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                dbManager.DropTable(tableName);
                MessageBox.Show("Таблица успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var tables = dbManager.GetUserTables();
                listBoxTables.Items.Clear();
                listBoxTables.Items.AddRange(tables.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении таблицы:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbManager?.Disconnect();
        }
    }
}
