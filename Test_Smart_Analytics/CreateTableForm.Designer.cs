namespace Test_Smart_Analytics
{
    partial class CreateTableForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Очистка всех ресурсов.
        /// </summary>
        /// <param name="disposing">true, если управляемые ресурсы должны быть освобождены; иначе false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Метод, вызываемый конструктором InitializeComponent().
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // CreateTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 450);
            Name = "CreateTableForm";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Создание таблицы";
            ResumeLayout(false);
        }
    }
}
