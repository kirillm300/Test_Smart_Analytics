using System.Data;
using System.Windows.Forms;

namespace Test_Smart_Analytics
{
    public partial class MainForm : Form
    {
        private DatabaseManager? dbManager;

        public MainForm()
        {
            InitializeComponent();
            SetupLabels();
            SetupMenu();
        }
        private void SetupMenu()
        {
            // Создаём MenuStrip
            MenuStrip menu = new MenuStrip();

            // === Вкладка "Помощь" ===
            ToolStripMenuItem helpItem = new ToolStripMenuItem("Помощь");

            // Подпункт "О программе"
            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе");
            aboutItem.Click += (s, e) =>
            {
                MessageBox.Show(
                    "Это приложение для редактирования таблиц в базе данных - тестовое задание для Смарт Аналитикс." +
                    "\n" +
                    "\n" +
                    "Версия 1.0",
                    "О программе",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            // Подпункт "Руководство пользователя"
            ToolStripMenuItem guideItem = new ToolStripMenuItem("Руководство пользователя");
            guideItem.Click += (s, e) =>
            {
                MessageBox.Show(
                    "1. Выберите таблицу слева, чтобы увидеть её структуру справа.\n" +
                    "2. Для совершения действий с таблицами используйте контекстное меню, вызываемое ПКМ в области слева.\n" +
                    "3. Для редактирования таблицы выберите её, вызовите контекстное меню и нажмите 'Редактировать'.\n" +
                    "4. Для удаления таблицы выберите её, вызовите контекстное меню и нажмите 'Удалить', подтвердите удаление.\n" +
                    "5. Для создания таблицы вызовите контекстное меню в области слева и нажмите 'Добавить таблицу в БД'.\n" +
                    "6. При возникновении ошибок приложение покажет сообщения в диалоговом окне.\n" +
                    "7. Типы данных, доступные при создании/редактировании таблиц: integer, bigint, double precision, text, timestamp.\n" +
                    "8. Во время создания/редактирования таблиц можно менять порядок полей и задавать NOT NULL для каждого поля.",
                    "Руководство пользователя",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            // Добавляем подпункты в "Помощь"
            helpItem.DropDownItems.Add(aboutItem);
            helpItem.DropDownItems.Add(guideItem);

            // Добавляем вкладку "Помощь" в MenuStrip
            menu.Items.Add(helpItem);

            // Dock сверху
            menu.Dock = DockStyle.Top;

            // Объявляем как главное меню формы
            this.MainMenuStrip = menu;

            // Добавляем на форму
            this.Controls.Add(menu);
        }


        private void SetupLabels()
        {
            int labelHeight = 25;
            int spacing = 5; // небольшой отступ между меткой и контентом

            // === Левая часть (Список таблиц) ===
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

            // Настраиваем ListBox
            listBoxTables.Location = new Point(0, lblTables.Height + spacing);
            listBoxTables.Width = splitContainerMain.Panel1.ClientSize.Width;
            listBoxTables.Height = splitContainerMain.Panel1.ClientSize.Height - lblTables.Height - spacing;
            listBoxTables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            splitContainerMain.Panel1.Controls.Add(listBoxTables);


            // === Правая часть (Структура таблицы) ===
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

            // Настраиваем DataGridView
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
                string connString = "Host=localhost;Port=5432;Database=Test;Username=postgres;Password=145311";
                dbManager = new DatabaseManager(connString);
                dbManager.Connect();

                var tables = dbManager.GetUserTables();
                listBoxTables.Items.Clear();
                listBoxTables.Items.AddRange(tables.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к БД: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dbManager == null || listBoxTables.SelectedItem == null)
                return;

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

                    // Обновляем список таблиц
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

                // Обновляем список таблиц
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
