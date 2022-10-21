namespace WindowFinder
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
            this.ChangeHotKeyButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ClearWithEscapeRadioButton = new System.Windows.Forms.RadioButton();
            this.HideWithEscRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearSearchOnFocusCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.TipTextLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PaddingTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TopRight = new System.Windows.Forms.Button();
            this.TopCenter = new System.Windows.Forms.Button();
            this.TopLeft = new System.Windows.Forms.Button();
            this.MiddleRight = new System.Windows.Forms.Button();
            this.MiddleCenter = new System.Windows.Forms.Button();
            this.MiddleLeft = new System.Windows.Forms.Button();
            this.BottomRight = new System.Windows.Forms.Button();
            this.BottomCenter = new System.Windows.Forms.Button();
            this.BottomLeft = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.HotKeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotKeyTextBox_KeyDown);
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
            // ChangeHotKeyButton
            // 
            this.ChangeHotKeyButton.Location = new System.Drawing.Point(250, 26);
            this.ChangeHotKeyButton.Name = "ChangeHotKeyButton";
            this.ChangeHotKeyButton.Size = new System.Drawing.Size(94, 29);
            this.ChangeHotKeyButton.TabIndex = 6;
            this.ChangeHotKeyButton.Text = "Change";
            this.ChangeHotKeyButton.UseVisualStyleBackColor = true;
            this.ChangeHotKeyButton.Click += new System.EventHandler(this.ChangeHotKeyButton_Click);
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
            this.groupBox2.Controls.Add(this.ChangeHotKeyButton);
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
            this.groupBox3.Controls.Add(this.ClearSearchOnFocusCheckbox);
            this.groupBox3.Controls.Add(this.HideOnFocusLostCheckbox);
            this.groupBox3.Controls.Add(this.ConsiderFullScreenCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 233);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(437, 142);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other";
            // 
            // ClearSearchOnFocusCheckbox
            // 
            this.ClearSearchOnFocusCheckbox.AutoSize = true;
            this.ClearSearchOnFocusCheckbox.Location = new System.Drawing.Point(22, 92);
            this.ClearSearchOnFocusCheckbox.Name = "ClearSearchOnFocusCheckbox";
            this.ClearSearchOnFocusCheckbox.Size = new System.Drawing.Size(366, 24);
            this.ClearSearchOnFocusCheckbox.TabIndex = 6;
            this.ClearSearchOnFocusCheckbox.Text = "Clear Search box when selected window is focused";
            this.ClearSearchOnFocusCheckbox.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(327, 598);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(135, 29);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // TipTextLabel
            // 
            this.TipTextLabel.AutoSize = true;
            this.TipTextLabel.Location = new System.Drawing.Point(14, 587);
            this.TipTextLabel.Name = "TipTextLabel";
            this.TipTextLabel.Size = new System.Drawing.Size(307, 40);
            this.TipTextLabel.TabIndex = 12;
            this.TipTextLabel.Text = "Tip: You can open this window with CTRL + O\r\nwhile the Search is open!";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.PaddingTextBox);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.TopRight);
            this.groupBox4.Controls.Add(this.TopCenter);
            this.groupBox4.Controls.Add(this.TopLeft);
            this.groupBox4.Controls.Add(this.MiddleRight);
            this.groupBox4.Controls.Add(this.MiddleCenter);
            this.groupBox4.Controls.Add(this.MiddleLeft);
            this.groupBox4.Controls.Add(this.BottomRight);
            this.groupBox4.Controls.Add(this.BottomCenter);
            this.groupBox4.Controls.Add(this.BottomLeft);
            this.groupBox4.Location = new System.Drawing.Point(12, 381);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(437, 203);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "pixels";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Padding: ";
            // 
            // PaddingTextBox
            // 
            this.PaddingTextBox.Location = new System.Drawing.Point(315, 131);
            this.PaddingTextBox.Name = "PaddingTextBox";
            this.PaddingTextBox.Size = new System.Drawing.Size(43, 27);
            this.PaddingTextBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 100);
            this.label3.TabIndex = 10;
            this.label3.Text = "Press the button that corresponds to your \r\ndesired position on the screen. \r\n\r\n(" +
    "Space between the window \r\nand the edge of your screen)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Search Window Position: ";
            // 
            // TopRight
            // 
            this.TopRight.Location = new System.Drawing.Point(389, 26);
            this.TopRight.Name = "TopRight";
            this.TopRight.Size = new System.Drawing.Size(33, 29);
            this.TopRight.TabIndex = 8;
            this.TopRight.TabStop = false;
            this.TopRight.Text = "┑";
            this.TopRight.UseVisualStyleBackColor = true;
            this.TopRight.Click += new System.EventHandler(this.TopRight_Click);
            // 
            // TopCenter
            // 
            this.TopCenter.Location = new System.Drawing.Point(350, 26);
            this.TopCenter.Name = "TopCenter";
            this.TopCenter.Size = new System.Drawing.Size(33, 29);
            this.TopCenter.TabIndex = 7;
            this.TopCenter.TabStop = false;
            this.TopCenter.Text = "─";
            this.TopCenter.UseVisualStyleBackColor = true;
            this.TopCenter.Click += new System.EventHandler(this.TopCenter_Click);
            // 
            // TopLeft
            // 
            this.TopLeft.Location = new System.Drawing.Point(311, 26);
            this.TopLeft.Name = "TopLeft";
            this.TopLeft.Size = new System.Drawing.Size(33, 29);
            this.TopLeft.TabIndex = 6;
            this.TopLeft.TabStop = false;
            this.TopLeft.Text = "┍";
            this.TopLeft.UseVisualStyleBackColor = true;
            this.TopLeft.Click += new System.EventHandler(this.TopLeft_Click);
            // 
            // MiddleRight
            // 
            this.MiddleRight.Location = new System.Drawing.Point(389, 61);
            this.MiddleRight.Name = "MiddleRight";
            this.MiddleRight.Size = new System.Drawing.Size(33, 29);
            this.MiddleRight.TabIndex = 5;
            this.MiddleRight.TabStop = false;
            this.MiddleRight.Text = "│";
            this.MiddleRight.UseVisualStyleBackColor = true;
            this.MiddleRight.Click += new System.EventHandler(this.MiddleRight_Click);
            // 
            // MiddleCenter
            // 
            this.MiddleCenter.Location = new System.Drawing.Point(350, 61);
            this.MiddleCenter.Name = "MiddleCenter";
            this.MiddleCenter.Size = new System.Drawing.Size(33, 29);
            this.MiddleCenter.TabIndex = 4;
            this.MiddleCenter.TabStop = false;
            this.MiddleCenter.Text = "╪";
            this.MiddleCenter.UseVisualStyleBackColor = true;
            this.MiddleCenter.Click += new System.EventHandler(this.MiddleCenter_Click);
            // 
            // MiddleLeft
            // 
            this.MiddleLeft.Location = new System.Drawing.Point(311, 61);
            this.MiddleLeft.Name = "MiddleLeft";
            this.MiddleLeft.Size = new System.Drawing.Size(33, 29);
            this.MiddleLeft.TabIndex = 3;
            this.MiddleLeft.TabStop = false;
            this.MiddleLeft.Text = "│";
            this.MiddleLeft.UseVisualStyleBackColor = true;
            this.MiddleLeft.Click += new System.EventHandler(this.MiddleLeft_Click);
            // 
            // BottomRight
            // 
            this.BottomRight.Location = new System.Drawing.Point(389, 96);
            this.BottomRight.Name = "BottomRight";
            this.BottomRight.Size = new System.Drawing.Size(33, 29);
            this.BottomRight.TabIndex = 2;
            this.BottomRight.TabStop = false;
            this.BottomRight.Text = "┙";
            this.BottomRight.UseVisualStyleBackColor = true;
            this.BottomRight.Click += new System.EventHandler(this.BottomRight_Click);
            // 
            // BottomCenter
            // 
            this.BottomCenter.Location = new System.Drawing.Point(350, 96);
            this.BottomCenter.Name = "BottomCenter";
            this.BottomCenter.Size = new System.Drawing.Size(33, 29);
            this.BottomCenter.TabIndex = 1;
            this.BottomCenter.TabStop = false;
            this.BottomCenter.Text = "━";
            this.BottomCenter.UseVisualStyleBackColor = true;
            this.BottomCenter.Click += new System.EventHandler(this.BottomCenter_Click);
            // 
            // BottomLeft
            // 
            this.BottomLeft.Location = new System.Drawing.Point(311, 96);
            this.BottomLeft.Name = "BottomLeft";
            this.BottomLeft.Size = new System.Drawing.Size(33, 29);
            this.BottomLeft.TabIndex = 0;
            this.BottomLeft.TabStop = false;
            this.BottomLeft.Text = "┕";
            this.BottomLeft.UseVisualStyleBackColor = true;
            this.BottomLeft.Click += new System.EventHandler(this.BottomLeft_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 640);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.TipTextLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox LaunchOnStartupCheckbox;
        private TextBox HotKeyTextBox;
        private Label label1;
        private CheckBox ConsiderFullScreenCheckBox;
        private CheckBox HideOnFocusLostCheckbox;
        private Button ChangeHotKeyButton;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button SaveButton;
        private Label TipTextLabel;
        private RadioButton ClearWithEscapeRadioButton;
        private RadioButton HideWithEscRadioButton;
        private CheckBox ClearSearchOnFocusCheckbox;
        private GroupBox groupBox4;
        private Label label5;
        private Label label4;
        private TextBox PaddingTextBox;
        private Label label3;
        private Label label2;
        private Button TopRight;
        private Button TopCenter;
        private Button TopLeft;
        private Button MiddleRight;
        private Button MiddleCenter;
        private Button MiddleLeft;
        private Button BottomRight;
        private Button BottomCenter;
        private Button BottomLeft;
    }
}