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
            components = new System.ComponentModel.Container();
            splitContainerMain = new SplitContainer();
            listBoxTables = new ListBox();
            contextMenuStripTables = new ContextMenuStrip(components);
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            createToolStripMenuItem = new ToolStripMenuItem();
            dataGridViewStructure = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            contextMenuStripTables.SuspendLayout();
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
            splitContainerMain.Size = new Size(1184, 745);
            splitContainerMain.SplitterDistance = 297;
            splitContainerMain.TabIndex = 2;
            // 
            // listBoxTables
            // 
            listBoxTables.ContextMenuStrip = contextMenuStripTables;
            listBoxTables.Dock = DockStyle.Fill;
            listBoxTables.FormattingEnabled = true;
            listBoxTables.ItemHeight = 15;
            listBoxTables.Location = new Point(0, 0);
            listBoxTables.Name = "listBoxTables";
            listBoxTables.Size = new Size(297, 745);
            listBoxTables.TabIndex = 0;
            listBoxTables.SelectedIndexChanged += listBoxTables_SelectedIndexChanged;
            // 
            // contextMenuStripTables
            // 
            contextMenuStripTables.ImageScalingSize = new Size(20, 20);
            contextMenuStripTables.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem, deleteToolStripMenuItem, createToolStripMenuItem });
            contextMenuStripTables.Name = "contextMenuStripTables";
            contextMenuStripTables.Size = new Size(203, 70);
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(202, 22);
            editToolStripMenuItem.Text = "Редактировать таблицу";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(202, 22);
            deleteToolStripMenuItem.Text = "Удалить таблицу";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new Size(202, 22);
            createToolStripMenuItem.Text = "Добавить таблицу в БД";
            createToolStripMenuItem.Click += createToolStripMenuItem_Click;
            // 
            // dataGridViewStructure
            // 
            dataGridViewStructure.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStructure.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStructure.Dock = DockStyle.Fill;
            dataGridViewStructure.Location = new Point(0, 0);
            dataGridViewStructure.Name = "dataGridViewStructure";
            dataGridViewStructure.ReadOnly = true;
            dataGridViewStructure.RowHeadersWidth = 51;
            dataGridViewStructure.Size = new Size(883, 745);
            dataGridViewStructure.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 745);
            Controls.Add(splitContainerMain);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление БД";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            contextMenuStripTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewStructure).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxTables;
        private SplitContainer splitContainerMain;
        private DataGridView dataGridViewStructure;
        private ContextMenuStrip contextMenuStripTables;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem createToolStripMenuItem;
    }
}
