using System.Runtime.InteropServices;

namespace WindowFinder
{
    public partial class Options : Form
    {
        private bool _isHotKeyChanged = false;
        private int keyModifier = 0;
        private uint hotKey = 0;

        private enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        // Win imports
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk); //handle, Id of hotkey, modifier (e.g ALT + DEL), hotkey key
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Options()
        {
            InitializeComponent();
        }

        // wrapper for Win API calls related to setting the global hotkey
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
                    // get key code/modifiers
                    Properties.Settings.Default["HotKey"] = hotKey;
                    Properties.Settings.Default["Modifiers"] = (uint)keyModifier;

                    // disable hotkey input for saftey
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

            // Set settings to component states
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
                    // Try to get Registry Key used for Startup Applications
                    Microsoft.Win32.RegistryKey? rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (rk == null)
                    {
                        MessageBox.Show("Could not add to startup. Please try again. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Set Registry Key
                        rk.SetValue("WindowFinder", Application.ExecutablePath.ToString());
                    }
                }
                else
                {
                    // Same as above but only delete
                    Microsoft.Win32.RegistryKey? rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (rk != null)
                    {
                        rk.DeleteValue("WindowFinder", false);
                    }
                }
            }

            Properties.Settings.Default["ClearSearchOnFocus"] = ClearSearchOnFocusCheckbox.Checked;
            Properties.Settings.Default["Padding"] =int.Parse(PaddingTextBox.Text);
            // Actually save the settings
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings saved! Application will restart to apply changes.");

            // Restart is needed for some settings to take effect
            Application.Restart();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            //// Set state of options to current settings
            LaunchOnStartupCheckbox.Checked = (bool)Properties.Settings.Default["LaunchOnStartup"];
            ConsiderFullScreenCheckBox.Checked = (bool)Properties.Settings.Default["ConsiderFullScreen"];

            // convert setting to bool
            var hideOnESC = (bool)Properties.Settings.Default["HideSearchWithEscape"];
            HideWithEscRadioButton.Checked = hideOnESC;
            ClearWithEscapeRadioButton.Checked = !hideOnESC;
            HideOnFocusLostCheckbox.Checked = (bool)Properties.Settings.Default["HideOnFocusLost"];


            var hotKey = (uint)Properties.Settings.Default["HotKey"];
            var modifiers = (uint)Properties.Settings.Default["Modifiers"];

            // Some bit math to get modifier for display
            uint mod = (uint)modifiers & 0xFFFF;
            Keys hotKeyKey = (Keys)hotKey;

            // use converter to print the character of the hotkey
            var converter = new KeysConverter();
            HotKeyTextBox.Text = (KeyModifier)mod + " + " + converter.ConvertToString(hotKeyKey);

            ClearSearchOnFocusCheckbox.Checked = Properties.Settings.Default["ClearSearchOnFocus"].ToString() == "True";

            PaddingTextBox.Text = Properties.Settings.Default.Padding.ToString();
            var LCR = Properties.Settings.Default.PositionLCR;
            var TCB = Properties.Settings.Default.PositionTCB;
            DisableChosenPositionButton(LCR, TCB);
        }

        private void DisableChosenPositionButton(int LCR, int TCB)
        {
            if (TCB == -1 && LCR == -1)
            {
                TopLeft.Enabled = false;
            }
            else if (TCB == -1 && LCR == 0)
            {
                TopCenter.Enabled = false;
            }
            else if (TCB == -1 && LCR == 1)
            {
                TopRight.Enabled = false;
            }
            else if (TCB == 0 && LCR == -1)
            {
                MiddleLeft.Enabled = false;
            }
            else if (TCB == 0 && LCR == 0)
            {
                MiddleCenter.Enabled = false;
                PaddingTextBox.Enabled = false;
            }
            else if (TCB == 1 && LCR == -1)
            {
                BottomLeft.Enabled = false;
            }
            else if (TCB == 1 && LCR == 0)
            {
                BottomCenter.Enabled = false;
            }
            else if (TCB == 1 && LCR == 1)
            {
                BottomRight.Enabled = false;
            }
        }

        private void HotKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // extract pressed keys from event in various formats
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData ^ modifierKeys;
            keyModifier = (int)KeyModifier.None;
            hotKey = (uint)pressedKey.GetHashCode();

            // converter used for display
            var converter = new KeysConverter();

            var pressedHotKey = e.KeyCode;

            // ignore text if it is just the modifier key
            if (pressedKey == Keys.Menu || pressedKey == Keys.ControlKey || pressedKey == Keys.ShiftKey || pressedKey == Keys.LWin)
            {
                e.SuppressKeyPress = true;
                HotKeyTextBox.Text = converter.ConvertToString(pressedKey);
                return;
            }

            // the modifier is comprised of 4 bits which make up a combination of the different
            // modifiers (e.g. ALT + DEL)
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

            // we set this to true so that Save knows to save it
            // I chose to do it this way because the display hot key is not the same
            // as what is saved in settings.
            _isHotKeyChanged = true;

            // We do not want this key event to continue being proccessed
            e.SuppressKeyPress = true;
        }

        private void ChangeHotKeyButton_Click(object sender, EventArgs e)
        {
            // Enable the hotkey text box
            HotKeyTextBox.Enabled = !HotKeyTextBox.Enabled;

            // Put the cursor in the textbox so that the user can just start typing
            HotKeyTextBox.Focus();
        }

        private void TopLeft_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = -1;
            Properties.Settings.Default.PositionTCB = -1;
            TopLeft.Enabled = false;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void TopCenter_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = 0;
            Properties.Settings.Default.PositionTCB = -1;
            TopLeft.Enabled = true;
            TopCenter.Enabled = false;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void TopRight_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = 1;
            Properties.Settings.Default.PositionTCB = -1;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = false;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void MiddleRight_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = 1;
            Properties.Settings.Default.PositionTCB = 0;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = false;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void MiddleCenter_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = 0;
            Properties.Settings.Default.PositionTCB = 0;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = false;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = false;
        }

        private void MiddleLeft_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = -1;
            Properties.Settings.Default.PositionTCB = 0;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = false;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void BottomLeft_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = -1;
            Properties.Settings.Default.PositionTCB = 1;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = false;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void BottomCenter_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = 0;
            Properties.Settings.Default.PositionTCB = 1;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = false;
            BottomRight.Enabled = true;
            PaddingTextBox.Enabled = true;
        }

        private void BottomRight_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PositionLCR = 1;
            Properties.Settings.Default.PositionTCB = 1;
            TopLeft.Enabled = true;
            TopCenter.Enabled = true;
            TopRight.Enabled = true;
            MiddleLeft.Enabled = true;
            MiddleCenter.Enabled = true;
            MiddleRight.Enabled = true;
            BottomLeft.Enabled = true;
            BottomCenter.Enabled = true;
            BottomRight.Enabled = false;
            PaddingTextBox.Enabled = true;
        }
    }
}
