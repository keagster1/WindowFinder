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

        private void SaveButton_Click(object sender, EventArgs e)
        {

            // check if hotkey was changed
            if (_isHotKeyChanged)
            {
                if (hotKey != 0)
                {
                    Properties.Settings.Default["HotKey"] = hotKey;
                    Properties.Settings.Default["Modifiers"] = (uint)keyModifier;
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
            
            HideOnFocusLostCheckbox.Checked = (bool)Properties.Settings.Default["HideOnFocusLost"];
            var converter = new KeysConverter();
            
            var hotKey = (uint)Properties.Settings.Default["HotKey"];
            var modifiers = (uint)Properties.Settings.Default["Modifiers"];
            
            uint mod = (uint)modifiers & 0xFFFF;
            Keys hotKeyKey = (Keys)hotKey;
            
            HotKeyTextBox.Text = (KeyModifier)mod + " + " + converter.ConvertToString(hotKeyKey);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HotKeyTextBox.Enabled = !HotKeyTextBox.Enabled;
            HotKeyTextBox.Focus();
        }
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        int keyModifier = 0;
        uint hotKey = 0;
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HotKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData ^ modifierKeys;
            keyModifier = (int)KeyModifier.None;
            hotKey = (uint)pressedKey.GetHashCode();

            var converter = new KeysConverter();
            var pressedHotKey = e.KeyCode;
            if (pressedKey == Keys.Menu || pressedKey == Keys.ControlKey || pressedKey == Keys.ShiftKey || pressedKey == Keys.LWin)
            {
                e.SuppressKeyPress = true;
                HotKeyTextBox.Text = converter.ConvertToString(pressedKey);
                return;
            }

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

            HotKeyTextBox.Text = converter.ConvertToString(pressedHotKey);

            _isHotKeyChanged = true;

            e.SuppressKeyPress = true;
        }
    }
}
