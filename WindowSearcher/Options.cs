using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowSearcher
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["LaunchOnStartup"] = LaunchOnStartupCheckbox.Checked;
            Properties.Settings.Default["ConsiderFullScreen"] = ConsiderFullScreenCheckBox.Checked;
            Properties.Settings.Default["HideSearchWithEscape"] = HideSearchWithEscapeCheckbox.Checked;
            Properties.Settings.Default["HideOnFocusLost"] = HideOnFocusLostCheckbox.Checked;

            uint hotKey = (uint)Properties.Settings.Default["HotKey"];
            uint modifiers = (uint)Properties.Settings.Default["Modifiers"];
            
            if (hotKey != 0)
            {
                Properties.Settings.Default["HotKey"] = hotKey;
                Properties.Settings.Default["Modifiers"] = modifiers;
                HotKeyTextBox.Enabled = false;
            } else
            {
                MessageBox.Show("You cannot set just a modifier or just a hotkey. Please set both.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Debug.WriteLine("Setting hot key as: " + hotKey + " and modifiers as: " + modifiers);
            // register 
            HotKeys.SetHotKey(this.Handle, modifiers, hotKey);

            //Properties.Settings.Default["OpenSearchHotkey"] = HotKeyTextBox.Text;

            Properties.Settings.Default.Save();
            MessageBox.Show("Settings saved! Please restart the application for changes to take effect.");
        }

        private void Options_Load(object sender, EventArgs e)
        {
            LaunchOnStartupCheckbox.Checked = (bool)Properties.Settings.Default["LaunchOnStartup"];
            ConsiderFullScreenCheckBox.Checked = (bool)Properties.Settings.Default["ConsiderFullScreen"];
            HideSearchWithEscapeCheckbox.Checked = (bool)Properties.Settings.Default["HideSearchWithEscape"];
            HideOnFocusLostCheckbox.Checked = (bool)Properties.Settings.Default["HideOnFocusLost"];
            var converter = new KeysConverter();

            // print hot key from settings
            var hotKey = (uint)Properties.Settings.Default["HotKey"];
            var modifiers = (uint)Properties.Settings.Default["Modifiers"];

            // convert keycode to character
            uint mod = (uint)modifiers & 0xFFFF;
            Keys hotKeyKey = (Keys)hotKey;
            
            HotKeyTextBox.Text = (KeyModifier)mod + " + " + converter.ConvertToString(hotKeyKey);

            String hotKeyTip = "You can open this Options screen with CTRL+O/nwhile you are using the Search!";

            String cantCloseWarning = "If you disable both options for hiding the Search you will not be able to close it without using the Task Manager!";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HotKeyTextBox.Enabled = !HotKeyTextBox.Enabled;
        }
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData ^ modifierKeys;
            int keyModifier = (int)KeyModifier.None;
            uint hotKey = (uint)pressedKey.GetHashCode();

            //print modifier as 
            var modifiers = modifierKeys;
            

            var converter = new KeysConverter();
            var pressedHotKey = e.KeyCode;
            Debug.WriteLine("KeyCode: " + pressedHotKey + " with modifier " + modifierKeys);
            if (pressedKey == Keys.Menu || pressedKey == Keys.ControlKey || pressedKey == Keys.ShiftKey || pressedKey == Keys.LWin)
            {
                //pressedModifiers = pressedKey;
                e.SuppressKeyPress = true;
                HotKeyTextBox.Text = converter.ConvertToString(pressedKey);
                return;
            }

            // get modifier keys as hex
            if (e.Control)
            {
                keyModifier += (int)KeyModifier.Control;
                pressedHotKey = pressedHotKey ^ Keys.Control;
            }
            if (e.Alt)
            {
                keyModifier += (int)KeyModifier.Alt;
                pressedHotKey = pressedHotKey ^ Keys.Alt;
            }
            if (e.Shift)
            {
                keyModifier += (int)KeyModifier.Shift;
                pressedHotKey = pressedHotKey ^ Keys.Shift;
            }

            //Debug.WriteLine(ToHex((int)pressedHotKey));
            HotKeyTextBox.Text = converter.ConvertToString(pressedHotKey);
            Debug.WriteLine("Detected" + hotKey.ToString() + " and " + keyModifier);
            Properties.Settings.Default["HotKey"] = (uint)hotKey;
            Properties.Settings.Default["Modifiers"] = (uint)keyModifier;
            
            e.SuppressKeyPress = true;
        }

        public string ToHex(int value)
        {
            return String.Format("0x{0:X}", value);
        }

        private void HotKeyTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
