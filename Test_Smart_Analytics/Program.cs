namespace Test_Smart_Analytics
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Сначала показываем окно выбора базы
            using (var dbForm = new DatabaseSelectionForm())
            {
                if (dbForm.ShowDialog() != DialogResult.OK)
                    return;

                string dbName = dbForm.DatabaseName;

                // Передаем имя БД в MainForm
                Application.Run(new MainForm(dbName));
            }
        }
    }
}