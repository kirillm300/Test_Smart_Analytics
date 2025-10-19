namespace Test_Smart_Analytics
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ������� ���������� ���� ������ ����
            using (var dbForm = new DatabaseSelectionForm())
            {
                if (dbForm.ShowDialog() != DialogResult.OK)
                    return;

                string dbName = dbForm.DatabaseName;

                // �������� ��� �� � MainForm
                Application.Run(new MainForm(dbName));
            }
        }
    }
}