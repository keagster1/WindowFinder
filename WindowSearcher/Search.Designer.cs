namespace WindowSearcher
{
    partial class Search
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.WindowFinderNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.TaskBarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindowDataGridView = new System.Windows.Forms.DataGridView();
            this.Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.WindowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskBarMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindowDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchTextBox.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchTextBox.Location = new System.Drawing.Point(0, 0);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(715, 47);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.SearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            this.SearchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchTextBox_KeyPress);
            // 
            // WindowFinderNotify
            // 
            this.WindowFinderNotify.ContextMenuStrip = this.TaskBarMenu;
            this.WindowFinderNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("WindowFinderNotify.Icon")));
            this.WindowFinderNotify.Text = "WindowFinder";
            this.WindowFinderNotify.Visible = true;
            // 
            // TaskBarMenu
            // 
            this.TaskBarMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TaskBarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.TaskBarMenu.Name = "TaskBarMenu";
            this.TaskBarMenu.Size = new System.Drawing.Size(184, 82);
            this.TaskBarMenu.Text = "WindowFinder";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.settingsToolStripMenuItem.Text = "Options";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // WindowDataGridView
            // 
            this.WindowDataGridView.AllowUserToAddRows = false;
            this.WindowDataGridView.AllowUserToDeleteRows = false;
            this.WindowDataGridView.AllowUserToResizeColumns = false;
            this.WindowDataGridView.AllowUserToResizeRows = false;
            this.WindowDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.WindowDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.WindowDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WindowDataGridView.ColumnHeadersVisible = false;
            this.WindowDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Icon,
            this.WindowName});
            this.WindowDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.WindowDataGridView.Location = new System.Drawing.Point(0, 48);
            this.WindowDataGridView.MultiSelect = false;
            this.WindowDataGridView.Name = "WindowDataGridView";
            this.WindowDataGridView.RowHeadersVisible = false;
            this.WindowDataGridView.RowHeadersWidth = 51;
            this.WindowDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.WindowDataGridView.RowTemplate.Height = 30;
            this.WindowDataGridView.RowTemplate.ReadOnly = true;
            this.WindowDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WindowDataGridView.ShowCellToolTips = false;
            this.WindowDataGridView.ShowEditingIcon = false;
            this.WindowDataGridView.ShowRowErrors = false;
            this.WindowDataGridView.Size = new System.Drawing.Size(715, 304);
            this.WindowDataGridView.TabIndex = 3;
            this.WindowDataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WindowDataGridView_MouseDoubleClick);
            // 
            // Icon
            // 
            this.Icon.HeaderText = "Icon";
            this.Icon.MinimumWidth = 6;
            this.Icon.Name = "Icon";
            this.Icon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Icon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Icon.Width = 6;
            // 
            // WindowName
            // 
            this.WindowName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WindowName.HeaderText = "WindowName";
            this.WindowName.MinimumWidth = 6;
            this.WindowName.Name = "WindowName";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 358);
            this.Controls.Add(this.WindowDataGridView);
            this.Controls.Add(this.SearchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(715, 358);
            this.MinimumSize = new System.Drawing.Size(715, 48);
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TaskBarMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WindowDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox SearchTextBox;
        private NotifyIcon WindowFinderNotify;
        private ContextMenuStrip TaskBarMenu;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private DataGridView WindowDataGridView;
        private DataGridViewImageColumn Icon;
        private DataGridViewTextBoxColumn WindowName;
    }
}