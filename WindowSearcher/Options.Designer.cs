namespace WindowSearcher
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.LaunchOnStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.HotKeyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConsiderFullScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.HideOnFocusLostCheckbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ClearWithEscapeRadioButton = new System.Windows.Forms.RadioButton();
            this.HideWithEscRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.TipTextLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LaunchOnStartupCheckbox
            // 
            this.LaunchOnStartupCheckbox.AutoSize = true;
            this.LaunchOnStartupCheckbox.Location = new System.Drawing.Point(6, 26);
            this.LaunchOnStartupCheckbox.Name = "LaunchOnStartupCheckbox";
            this.LaunchOnStartupCheckbox.Size = new System.Drawing.Size(150, 24);
            this.LaunchOnStartupCheckbox.TabIndex = 0;
            this.LaunchOnStartupCheckbox.Text = "Launch on Startup";
            this.LaunchOnStartupCheckbox.UseVisualStyleBackColor = true;
            // 
            // HotKeyTextBox
            // 
            this.HotKeyTextBox.Enabled = false;
            this.HotKeyTextBox.Location = new System.Drawing.Point(119, 28);
            this.HotKeyTextBox.Name = "HotKeyTextBox";
            this.HotKeyTextBox.Size = new System.Drawing.Size(125, 27);
            this.HotKeyTextBox.TabIndex = 1;
            this.HotKeyTextBox.TextChanged += new System.EventHandler(this.HotKeyTextBox_TextChanged);
            this.HotKeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Open Search: ";
            // 
            // ConsiderFullScreenCheckBox
            // 
            this.ConsiderFullScreenCheckBox.AutoSize = true;
            this.ConsiderFullScreenCheckBox.Location = new System.Drawing.Point(22, 32);
            this.ConsiderFullScreenCheckBox.Name = "ConsiderFullScreenCheckBox";
            this.ConsiderFullScreenCheckBox.Size = new System.Drawing.Size(412, 24);
            this.ConsiderFullScreenCheckBox.TabIndex = 3;
            this.ConsiderFullScreenCheckBox.Text = "Don\'t show search box when using Full Screen application";
            this.ConsiderFullScreenCheckBox.UseVisualStyleBackColor = true;
            this.ConsiderFullScreenCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // HideOnFocusLostCheckbox
            // 
            this.HideOnFocusLostCheckbox.AutoSize = true;
            this.HideOnFocusLostCheckbox.Location = new System.Drawing.Point(22, 62);
            this.HideOnFocusLostCheckbox.Name = "HideOnFocusLostCheckbox";
            this.HideOnFocusLostCheckbox.Size = new System.Drawing.Size(260, 24);
            this.HideOnFocusLostCheckbox.TabIndex = 5;
            this.HideOnFocusLostCheckbox.Text = "Hide Search box when focus is lost";
            this.HideOnFocusLostCheckbox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LaunchOnStartupCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 69);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Start Up";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ClearWithEscapeRadioButton);
            this.groupBox2.Controls.Add(this.HideWithEscRadioButton);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.HotKeyTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 138);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shortcuts";
            // 
            // ClearWithEscapeRadioButton
            // 
            this.ClearWithEscapeRadioButton.AutoSize = true;
            this.ClearWithEscapeRadioButton.Location = new System.Drawing.Point(22, 100);
            this.ClearWithEscapeRadioButton.Name = "ClearWithEscapeRadioButton";
            this.ClearWithEscapeRadioButton.Size = new System.Drawing.Size(251, 24);
            this.ClearWithEscapeRadioButton.TabIndex = 8;
            this.ClearWithEscapeRadioButton.TabStop = true;
            this.ClearWithEscapeRadioButton.Text = "Clear Search Box with Escape Key";
            this.ClearWithEscapeRadioButton.UseVisualStyleBackColor = true;
            // 
            // HideWithEscRadioButton
            // 
            this.HideWithEscRadioButton.AutoSize = true;
            this.HideWithEscRadioButton.Location = new System.Drawing.Point(22, 70);
            this.HideWithEscRadioButton.Name = "HideWithEscRadioButton";
            this.HideWithEscRadioButton.Size = new System.Drawing.Size(249, 24);
            this.HideWithEscRadioButton.TabIndex = 7;
            this.HideWithEscRadioButton.TabStop = true;
            this.HideWithEscRadioButton.Text = "Hide Search box with Escape Key";
            this.HideWithEscRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.HideOnFocusLostCheckbox);
            this.groupBox3.Controls.Add(this.ConsiderFullScreenCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 233);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(437, 118);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(314, 436);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(135, 29);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(211, 436);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 29);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TipTextLabel
            // 
            this.TipTextLabel.AutoSize = true;
            this.TipTextLabel.Location = new System.Drawing.Point(12, 354);
            this.TipTextLabel.Name = "TipTextLabel";
            this.TipTextLabel.Size = new System.Drawing.Size(307, 40);
            this.TipTextLabel.TabIndex = 12;
            this.TipTextLabel.Text = "Tip: You can open this window with CTRL + O\r\nwhile the Search is open!";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 482);
            this.Controls.Add(this.TipTextLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowFinder Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox LaunchOnStartupCheckbox;
        private TextBox HotKeyTextBox;
        private Label label1;
        private CheckBox ConsiderFullScreenCheckBox;
        private CheckBox HideOnFocusLostCheckbox;
        private Button button1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button SaveButton;
        private Button CancelButton;
        private Label TipTextLabel;
        private RadioButton ClearWithEscapeRadioButton;
        private RadioButton HideWithEscRadioButton;
    }
}