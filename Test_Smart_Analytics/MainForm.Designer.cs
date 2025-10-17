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
            menuStrip1 = new MenuStrip();
            nfToolStripMenuItem = new ToolStripMenuItem();
            createToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            dataGridViewStructure = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStructure).BeginInit();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Margin = new Padding(3, 4, 3, 4);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(listBoxTables);
            splitContainerMain.Panel1.Controls.Add(menuStrip1);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(dataGridViewStructure);
            splitContainerMain.Size = new Size(914, 600);
            splitContainerMain.SplitterDistance = 230;
            splitContainerMain.SplitterWidth = 5;
            splitContainerMain.TabIndex = 2;
            // 
            // listBoxTables
            // 
            listBoxTables.Dock = DockStyle.Fill;
            listBoxTables.FormattingEnabled = true;
            listBoxTables.Location = new Point(0, 30);
            listBoxTables.Margin = new Padding(3, 4, 3, 4);
            listBoxTables.Name = "listBoxTables";
            listBoxTables.Size = new Size(230, 570);
            listBoxTables.TabIndex = 0;
            listBoxTables.SelectedIndexChanged += listBoxTables_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { nfToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(230, 30);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // nfToolStripMenuItem
            // 
            nfToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem });
            nfToolStripMenuItem.Name = "nfToolStripMenuItem";
            nfToolStripMenuItem.Size = new Size(82, 24);
            nfToolStripMenuItem.Text = "Таблица";
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new Size(224, 26);
            createToolStripMenuItem.Text = "Создать";
            createToolStripMenuItem.Click += createToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(224, 26);
            editToolStripMenuItem.Text = "Редактировать";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(224, 26);
            deleteToolStripMenuItem.Text = "Удалить";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // dataGridViewStructure
            // 
            dataGridViewStructure.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStructure.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStructure.Dock = DockStyle.Fill;
            dataGridViewStructure.Location = new Point(0, 0);
            dataGridViewStructure.Margin = new Padding(3, 4, 3, 4);
            dataGridViewStructure.Name = "dataGridViewStructure";
            dataGridViewStructure.ReadOnly = true;
            dataGridViewStructure.RowHeadersWidth = 51;
            dataGridViewStructure.Size = new Size(679, 600);
            dataGridViewStructure.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(splitContainerMain);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Управление БД";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel1.PerformLayout();
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStructure).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxTables;
        private SplitContainer splitContainerMain;
        private DataGridView dataGridViewStructure;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem nfToolStripMenuItem;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
    }
}
