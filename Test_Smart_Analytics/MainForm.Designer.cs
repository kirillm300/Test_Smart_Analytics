namespace Test_Smart_Analytics
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainerMain = new SplitContainer();
            listBoxTables = new ListBox();
            dataGridViewStructure = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStructure).BeginInit();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(listBoxTables);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(dataGridViewStructure);
            splitContainerMain.Size = new Size(800, 450);
            splitContainerMain.SplitterDistance = 202;
            splitContainerMain.TabIndex = 2;
            // 
            // listBoxTables
            // 
            listBoxTables.Dock = DockStyle.Fill;
            listBoxTables.FormattingEnabled = true;
            listBoxTables.ItemHeight = 15;
            listBoxTables.Location = new Point(0, 0);
            listBoxTables.Name = "listBoxTables";
            listBoxTables.Size = new Size(202, 450);
            listBoxTables.TabIndex = 0;
            listBoxTables.SelectedIndexChanged += listBoxTables_SelectedIndexChanged;
            // 
            // dataGridViewStructure
            // 
            dataGridViewStructure.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStructure.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStructure.Dock = DockStyle.Fill;
            dataGridViewStructure.Location = new Point(0, 0);
            dataGridViewStructure.Name = "dataGridViewStructure";
            dataGridViewStructure.ReadOnly = true;
            dataGridViewStructure.Size = new Size(594, 450);
            dataGridViewStructure.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainerMain);
            Name = "MainForm";
            Text = "Управление БД";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewStructure).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxTables;
        private SplitContainer splitContainerMain;
        private DataGridView dataGridViewStructure;
    }
}
