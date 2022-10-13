﻿namespace WindowSearcher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.TaskBarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindowListBox = new System.Windows.Forms.ListBox();
            this.TaskBarMenu.SuspendLayout();
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
            this.SearchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchTextBox_KeyPress);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.TaskBarMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // TaskBarMenu
            // 
            this.TaskBarMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TaskBarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.TaskBarMenu.Name = "TaskBarMenu";
            this.TaskBarMenu.Size = new System.Drawing.Size(115, 58);
            this.TaskBarMenu.Text = "WindowFinder";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(111, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // WindowListBox
            // 
            this.WindowListBox.FormattingEnabled = true;
            this.WindowListBox.ItemHeight = 20;
            this.WindowListBox.Location = new System.Drawing.Point(0, 49);
            this.WindowListBox.MaximumSize = new System.Drawing.Size(715, 358);
            this.WindowListBox.MinimumSize = new System.Drawing.Size(715, 48);
            this.WindowListBox.Name = "WindowListBox";
            this.WindowListBox.Size = new System.Drawing.Size(715, 304);
            this.WindowListBox.TabIndex = 1;
            this.WindowListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WindowListBox_MouseDoubleClick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 358);
            this.Controls.Add(this.WindowListBox);
            this.Controls.Add(this.SearchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(715, 358);
            this.MinimumSize = new System.Drawing.Size(715, 48);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TaskBarMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox SearchTextBox;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip TaskBarMenu;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ListBox WindowListBox;
    }
}