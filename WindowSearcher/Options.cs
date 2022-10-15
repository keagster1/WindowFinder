using System.Runtime.InteropServices;

namespace WindowFinder
{
    public partial class Options : Form
    {
        private enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        
        private bool _isHotKeyChanged = false;

        private int keyModifier = 0;
        private uint hotKey = 0;
        public Options()
        {
            InitializeComponent();
        }
        
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk); //handle, Id of hotkey, modifier (e.g ALT + DEL), hotkey key
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public static void SetHotKey(IntPtr hWnd, uint modifiers, uint keyHashCode)
        {
            UnregisterHotKey(hWnd, 0);
            RegisterHotKey(hWnd, 0, modifiers, keyHashCode);
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
                    UnregisterHotKey(this.Handle, 0);
                    SetHotKey(this.Handle, 0, (uint)HotKeyTextBox.Text.ToUpper().ToCharArray()[0]);
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
                    
                    Microsoft.Win32.RegistryKey? rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (rk == null)
                    {
                        MessageBox.Show("Could not add to startup. Please try again. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        rk.SetValue("WindowFinder", Application.ExecutablePath.ToString());
                    }
                
                }
                else
                {
                    Microsoft.Win32.RegistryKey? rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (rk != null)
                    {
                        rk.DeleteValue("WindowFinder", false);
                    }                    
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
                pressedHotKey ^= Keys.Control;
            }
            if (e.Alt)
            {
                keyModifier += (int)KeyModifier.Alt;
                pressedHotKey ^= Keys.Alt;
            }
            if (e.Shift)
            {
                keyModifier += (int)KeyModifier.Shift;
                pressedHotKey ^= Keys.Shift;
            }

            HotKeyTextBox.Text = converter.ConvertToString(pressedHotKey);

            _isHotKeyChanged = true;

            e.SuppressKeyPress = true;
        }

        private void ChangeHotKeyButton_Click(object sender, EventArgs e)
        {
            HotKeyTextBox.Enabled = !HotKeyTextBox.Enabled;
            HotKeyTextBox.Focus();
        }
    }
}
