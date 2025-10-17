using System.Data;

namespace Test_Smart_Analytics
{
    public partial class MainForm : Form
    {
        private DatabaseManager? dbManager;

        public MainForm()
        {
            InitializeComponent();
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbManager?.Disconnect();
        }
    }
}
