namespace Test_Smart_Analytics
{
    partial class CreateTableForm
    {
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.TextBox txtTableName;
        public System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.txtTableName = new System.Windows.Forms.TextBox();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.lblTableName = new System.Windows.Forms.Label();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.SuspendLayout();

            this.lblTableName.Text = "Имя таблицы:";
            this.lblTableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTableName.Height = 25;

            this.txtTableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTableName.Height = 25;

            this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumns.AllowUserToAddRows = true;
            this.dgvColumns.AllowUserToDeleteRows = true;
            this.dgvColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvColumns.Columns.Add("Name", "Имя поля");
            var colType = new System.Windows.Forms.DataGridViewComboBoxColumn()
            {
                HeaderText = "Тип данных",
                Name = "Type",
                DataSource = new System.Collections.Generic.List<string> { "integer", "bigint", "double precision", "text", "timestamp" }
            };
            this.dgvColumns.Columns.Add(colType);
            this.dgvColumns.Columns.Add(new System.Windows.Forms.DataGridViewCheckBoxColumn()
            {
                HeaderText = "Первичный ключ",
                Name = "IsPrimaryKey"
            });
            this.dgvColumns.Columns.Add(new System.Windows.Forms.DataGridViewCheckBoxColumn()
            {
                HeaderText = "NOT NULL",
                Name = "NotNull"
            });

            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Height = 45;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;

            this.btnOk.Text = "Создать";
            this.btnOk.Height = 40;
            this.btnOk.Width = 100;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Height = 40;
            this.btnCancel.Width = 100;
            this.btnCancel.Click += (s, e) => this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.btnMoveUp.Text = "↑ Вверх";
            this.btnMoveUp.Height = 40;
            this.btnMoveUp.Width = 100;
            this.btnMoveUp.Click += new System.EventHandler(this.BtnMoveUp_Click);

            this.btnMoveDown.Text = "↓ Вниз";
            this.btnMoveDown.Height = 40;
            this.btnMoveDown.Width = 100;
            this.btnMoveDown.Click += new System.EventHandler(this.BtnMoveDown_Click);

            this.buttonsPanel.Controls.Add(this.btnCancel);
            this.buttonsPanel.Controls.Add(this.btnOk);
            this.buttonsPanel.Controls.Add(this.btnMoveDown);
            this.buttonsPanel.Controls.Add(this.btnMoveUp);

            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.Add(this.dgvColumns);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.buttonsPanel);
            this.Text = "Создание таблицы";

            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
