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
        private bool _isHotKeyChanged = false;
        public Options()
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            // check if hotkey was changed
            if (_isHotKeyChanged)
            {
                uint hotKey = (uint)Properties.Settings.Default["HotKey"];
                uint modifiers = (uint)Properties.Settings.Default["Modifiers"];

                if (hotKey != 0)
                {
                    Properties.Settings.Default["HotKey"] = hotKey;
                    Properties.Settings.Default["Modifiers"] = modifiers;
                    HotKeyTextBox.Enabled = false;
                    // if hotkey was changed, unregister old hotkey and register new hotkey
                    HotKeys.UnregisterHotKey(this.Handle, 0);
                    HotKeys.SetHotKey(this.Handle, 0, (uint)HotKeyTextBox.Text.ToUpper().ToCharArray()[0]);
                }
                else
                {
                    MessageBox.Show("You cannot set just a modifier or just a hotkey. Please set both.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Properties.Settings.Default["ConsiderFullScreen"] = ConsiderFullScreenCheckBox.Checked;
            Properties.Settings.Default["HideSearchWithEscape"] = HideWithEscRadioButton.Checked;
            Properties.Settings.Default["HideOnFocusLost"] = HideOnFocusLostCheckbox.Checked;

            
            // check if LaunchOnStartup changed
            if ((bool)Properties.Settings.Default["LaunchOnStartup"] != LaunchOnStartupCheckbox.Checked)
            {
                Properties.Settings.Default["LaunchOnStartup"] = LaunchOnStartupCheckbox.Checked;
                // if LaunchOnStartup changed, update registry
                if (LaunchOnStartupCheckbox.Checked)
                {
                    Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    rk.SetValue("WindowSearcher", Application.ExecutablePath.ToString());
                }
                else
                {
                    Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    rk.DeleteValue("WindowSearcher", false);
                }
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings saved! Application will restart to apply changes.");
            Application.Restart();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            LaunchOnStartupCheckbox.Checked = (bool)Properties.Settings.Default["LaunchOnStartup"];
            ConsiderFullScreenCheckBox.Checked = (bool)Properties.Settings.Default["ConsiderFullScreen"];

            // select radiobutton depending on what is saved in settings
            var hideOnESC = (bool)Properties.Settings.Default["HideSearchWithEscape"];
            if (hideOnESC)
            {
                HideWithEscRadioButton.Checked = true;
                ClearWithEscapeRadioButton.Checked = false;
            } else
            {
                HideWithEscRadioButton.Checked = false;
                ClearWithEscapeRadioButton.Checked = true;
            }

            //HideSearchWithEscapeCheckbox.Checked = (bool)Properties.Settings.Default["HideSearchWithEscape"];
            HideOnFocusLostCheckbox.Checked = (bool)Properties.Settings.Default["HideOnFocusLost"];
            var converter = new KeysConverter();

            // print hot key from settings
            var hotKey = (uint)Properties.Settings.Default["HotKey"];
            var modifiers = (uint)Properties.Settings.Default["Modifiers"];

            // convert keycode to character
            uint mod = (uint)modifiers & 0xFFFF;
            Keys hotKeyKey = (Keys)hotKey;
            
            HotKeyTextBox.Text = (KeyModifier)mod + " + " + converter.ConvertToString(hotKeyKey);
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
            _isHotKeyChanged = true;


            e.SuppressKeyPress = true;
        }

        public string ToHex(int value)
        {
            return String.Format("0x{0:X}", value);
        }

        private void HotKeyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
