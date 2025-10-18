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
            //splitContainerMain.Padding = new Padding(0, 100, 0, 0);
            SetupLabels();
        }

        private void SetupLabels()
        {
            int labelHeight = 25;
            int spacing = 5; // ��������� ������ ����� ������ � ���������

            // === ����� ����� (������ ������) ===
            Label lblTables = new Label
            {
                Text = "������ ������",
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = SystemColors.ControlLight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top
            };
            splitContainerMain.Panel1.Controls.Add(lblTables);

            // ����������� ListBox
            listBoxTables.Location = new Point(0, lblTables.Height + spacing);
            listBoxTables.Width = splitContainerMain.Panel1.ClientSize.Width;
            listBoxTables.Height = splitContainerMain.Panel1.ClientSize.Height - lblTables.Height - spacing;
            listBoxTables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            splitContainerMain.Panel1.Controls.Add(listBoxTables);


            // === ������ ����� (��������� �������) ===
            Label lblStructure = new Label
            {
                Text = "��������� �������",
                Height = labelHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = SystemColors.ControlLight,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top
            };
            splitContainerMain.Panel2.Controls.Add(lblStructure);

            // ����������� DataGridView
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
                MessageBox.Show($"������ ��� ����������� � ��: {ex.Message}",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"������ ��� �������� ��������� �������:\n{ex.Message}",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("������� ������� �������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ��������� ������ ������
                    var tables = dbManager.GetUserTables();
                    listBoxTables.Items.Clear();
                    listBoxTables.Items.AddRange(tables.ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ��� �������� �������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem == null)
            {
                MessageBox.Show("�������� ������� ��� ��������������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tableName = listBoxTables.SelectedItem.ToString();

            try
            {
                var columns = dbManager.GetTableColumns(tableName);

                using var form = new CreateTableForm();
                form.Text = $"�������������� �������: {tableName}";

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
                    MessageBox.Show("��������� ������� ���������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var tables = dbManager.GetUserTables();
                    listBoxTables.Items.Clear();
                    listBoxTables.Items.AddRange(tables.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������������� �������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem == null)
            {
                MessageBox.Show("�������� ������� ��� ��������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tableName = listBoxTables.SelectedItem.ToString();

            var confirm = MessageBox.Show(
                $"�� ������������� ������ ������� ������� \"{tableName}\"?\n��� ������ ����� ��������.",
                "������������� ��������",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                dbManager.DropTable(tableName);
                MessageBox.Show("������� ������� �������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ��������� ������ ������
                var tables = dbManager.GetUserTables();
                listBoxTables.Items.Clear();
                listBoxTables.Items.AddRange(tables.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� �������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbManager?.Disconnect();
        }
    }
}
