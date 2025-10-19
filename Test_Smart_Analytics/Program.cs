namespace Test_Smart_Analytics
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var dbForm = new DatabaseSelectionForm())
            {
                if (dbForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm(
                        dbForm.Host,
                        dbForm.Port,
                        dbForm.DatabaseName,
                        dbForm.Username,
                        dbForm.Password));
                }
            }
        }
    }
}