using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System;
using System.Windows.Forms;
using HWND = System.IntPtr;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.Common.Internal;
using Topshelf.Runtime.Windows;
using System.Windows.Input;

// TODO: Implement the rest of the settings
// TODO: Clean up inused variables
// TODO: Refactor like a mofo

namespace WindowSearcher
{
    public partial class Search : Form
    {
        public Dictionary<IntPtr, string> WindowList;
        public Search()
        {
            InitializeComponent();

            int id = 0;
            uint keyModifier = (int)KeyModifier.Alt;
            uint key = (uint)Keys.F1.GetHashCode();
            


            // check if settings has values
            if ((uint)Properties.Settings.Default["HotKey"] > 0 && (uint)Properties.Settings.Default["Modifiers"] > 0)
            {
                RegisterHotKey(this.Handle, id, (uint)Properties.Settings.Default["Modifiers"], (uint)Properties.Settings.Default["HotKey"]);
                Debug.WriteLine("Registering: " + id + " " + keyModifier + " " + key);
            } else
            {
                RegisterHotKey(this.Handle, id, keyModifier, key);
                Debug.WriteLine("Registering ALT + F1 as: " + id + " " + keyModifier + " " + key);
            }
        }

        // Registers a hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        // Unregisters the hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier { 
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<IntPtr, string> w = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            foreach (KeyValuePair<IntPtr, string> window in w)
            {
                WindowListBox.Items.Add(window.Value);
                //Debug.WriteLine("Added " + window.Value);
            }


            //SearchTextBox_TextChanged(sender, e);
            Resize();
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            const int WM_KEYDOWN = 0x0100;
            //const int WM_KEYUP = 0x0101;
            int msgVal = m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {              
                switch ((Keys)msgVal)
                { 
                    case Keys.Up:
                        MoveSelectedIndex(true);
                        break;

                    case Keys.Down:
                        MoveSelectedIndex(false);
                        break;
                    case Keys.Escape:
                        if ((bool)Properties.Settings.Default["HideSearchWithEscape"])
                        {
                            this.Hide();
                        }
                        else
                        {
                            SearchTextBox.Text = "";
                            ResetWindowList();
                            // resize listbox to fit 10 items
                            Resize();
                            return true;
                        }
                        break;
                    case Keys.Enter:

                        // get list of open windows
                        Dictionary<IntPtr, string> WindowList = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
                        Debug.WriteLine(SearchTextBox.Text);
                        if (SearchTextBox.Text.Trim().StartsWith("/"))
                        {
                            if (SearchTextBox.Text.Trim().Equals("/options")) {
                                Options o = new Options();
                                o.ShowDialog();
                                break;
                            } else if(SearchTextBox.Text.Trim().Equals("/exit"))
                            {
                                Application.Exit();
                                break;
                            }
                        }

                        // find handle for text
                        foreach (KeyValuePair<IntPtr, string> window in WindowList)
                        {
                            if (window.Value == WindowListBox.SelectedItem.ToString())
                            {

                                // set focus to window
                                FocusWindow(window.Key);
                                break;
                            }
                        }

                        break;
                }
            }

            return base.ProcessKeyPreview(ref m);
        }


        public bool hasChangedSelection = false;
        public void MoveSelectedIndex(bool is_going_up)
        {
            // check if anything is selected
            if (WindowListBox.SelectedItems.Count == 0)
            {
                if (WindowListBox.Items.Count != 0)
                {
                    WindowListBox.SelectedIndex = 0;
                    // increment selected item in windowlistview

                }
            }
            if (!is_going_up)
            {
                if (WindowListBox.SelectedIndex == WindowListBox.Items.Count - 1)
                {
                    WindowListBox.SelectedIndex = 0;
                }
                else
                {
                    if (hasChangedSelection)
                        WindowListBox.SelectedIndex += 1;
                    else
                    {
                        WindowListBox.SelectedIndex = 0;
                        hasChangedSelection = true;
                    }
                }
            }
            else
            {
                if (WindowListBox.SelectedIndex == 0)
                {
                    WindowListBox.SelectedIndex = WindowListBox.Items.Count - 1;
                }
                else
                {
                    WindowListBox.SelectedIndex -= 1;
                }
            }
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public void FocusWindow(IntPtr hWnd)
        {
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
                SetForegroundWindow(hWnd);
            }
            //BringWindowToTop(hWnd);
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool ShowWindowAsync(IntPtr windowHandle, int nCmdShow);

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            hasChangedSelection = false;
            if (SearchTextBox.Text == "")
            {
                // reset the list to all open windows
                WindowListBox.Items.Clear();
                Dictionary<IntPtr, string> w = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
                foreach (KeyValuePair<IntPtr, string> window in w)
                {
                    WindowListBox.Items.Add(window.Value);
                    //Debug.WriteLine("Added " + window.Value);
                }
                //Debug.WriteLine("reset");
                return;
            }

            ResetWindowList();
            // resize listbox to fit 10 items
            Resize();
        }

        public bool ListViewContainsString(ListView lv, string s)
        {
            foreach (ListViewItem lvi in lv.Items)
            {
                if (lvi.Text.Contains(s))
                    return true;
            }
            return false;
        }

        public void Resize()
        {
            WindowListBox.Size = new Size(WindowListBox.Size.Width, WindowListBox.Items.Count * 30);
            this.Size = new Size(this.Size.Width, WindowListBox.Size.Height + 50);
        }

        private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ResetWindowList()
        {
            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            // loop over all windows
            foreach (KeyValuePair<IntPtr, string> window in windows)
            {
                // if the window name matches the text in the combo box
                // perform regex search on the window name
                // convert SearchTextBox.text to regex
                // escape special characters
                string pattern = Regex.Escape(SearchTextBox.Text);

                if (Regex.IsMatch(window.Value, pattern, RegexOptions.IgnoreCase))
                {

                    if (!WindowListBox.Items.Contains(window.Value))
                    {
                        // add the window to the combo box
                        WindowListBox.Items.Add(window.Value);
                    }
                }
                else
                {
                    // hide the item in the combo box
                    WindowListBox.Items.Remove(window.Value);
                }

            }
        }

        public string ToHex(int value)
        {
            return String.Format("0x{0:X}", value);
        }

        private int WM_HOTKEY = 0x0312;
        protected override void WndProc(ref Message hotkey)
        {
            base.WndProc(ref hotkey);

            if (hotkey.Msg == WM_HOTKEY)
            {
                Keys key = (Keys)(((int)hotkey.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)hotkey.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = hotkey.WParam.ToInt32();

                Debug.WriteLine(key + " " + modifier + " " + id);

                this.Show();
                this.WindowState = FormWindowState.Normal;
                Search.SetForegroundWindow(Handle);

                WindowListBox.Items.Clear();
                Dictionary<IntPtr, string> w = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
                foreach (KeyValuePair<IntPtr, string> window in w)
                {
                    WindowListBox.Items.Add(window.Value);
                }
                SearchTextBox.Focus();
                hotkey.Result = (IntPtr)1;
            }
            const int WM_ACTIVATEAPP = 0x001C;
            if (hotkey.Msg == WM_ACTIVATEAPP)
            {
                if (hotkey.WParam.ToInt64() == 0)
                {
                    this.Hide();
                }
            }
            
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WindowListBox_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (WindowListBox.SelectedItems.Count == 0)
            {
                return;
            }
            string window_name;
            if (WindowListBox.SelectedItems[0].ToString() == "")
            {
                return;
            }


            window_name = WindowListBox.SelectedItems[0].ToString();


            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            foreach (KeyValuePair<IntPtr, string> window in windows)
            {
                if (window.Value == window_name)
                {
                    FocusWindow(window.Key);
                    this.Hide();
                    return;
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // check if ctrl+o is pressed
            if (e.Control && e.KeyCode == Keys.O)
            {
                // show the options form
                Options options = new Options();
                options.Show();
                e.SuppressKeyPress = true;
            }
        }
    }

    public static class OpenWindowGetter
    {
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;
            }, 0);

            return windows;
        }

        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        // import win32


        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

    }
}

