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
            WindowListView.FullRowSelect = true;
            WindowListView.GridLines = false;
            WindowListView.View = System.Windows.Forms.View.Details;
            WindowListView.Scrollable = true;
            WindowListView.Columns[0].Width = WindowListView.Width-30; // set width to size of listview
            
            Dictionary<IntPtr, string> w = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            foreach (KeyValuePair<IntPtr, string> window in w)
            {
                WindowListView.Items.Add(window.Value);
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
                        MoveViewSelectedIndex(true);
                        break;

                    case Keys.Down:
                        MoveViewSelectedIndex(false);
                        break;
                    case Keys.Escape:
                        if ((bool)Properties.Settings.Default["HideSearchWithEscape"])
                        {
                            this.Hide();
                            m.Result = (IntPtr)1;
                            return base.ProcessKeyPreview(ref m);
                        }
                        else
                        {
                            SearchTextBox.Text = "";
                            ResetWindowView();
                            // resize listbox to fit 10 items
                            Resize();
                            m.Result = (IntPtr)1;
                            return base.ProcessKeyPreview(ref m);
                        }
                    case Keys.Enter:

                        // get list of open windows
                        Dictionary<IntPtr, string> WindowList = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
                        Debug.WriteLine(SearchTextBox.Text);
                        if (SearchTextBox.Text.Trim().StartsWith("/"))
                        {
                            if (SearchTextBox.Text.Trim().Equals("/options")) {
                                Options o = new Options();
                                o.ShowDialog();
                            } else if(SearchTextBox.Text.Trim().Equals("/exit"))
                            {
                                Application.Exit();
                                break;
                            }
                        }

                        // find handle for text
                        foreach (KeyValuePair<IntPtr, string> window in WindowList)
                        {
                            if (WindowListView.SelectedItems.Count == 0)
                            {
                                break;
                            }
                            if (window.Value.Equals(WindowListView.SelectedItems[0].Text))
                            {

                                // set focus to window
                                FocusWindow(window.Key);
                                // supress key event
                                m.Result = (IntPtr)1;
                                return true;
                            }
                        }

                        break;
                }
            }

            return base.ProcessKeyPreview(ref m);
        }


        public bool hasChangedSelection = false;
        
        public void MoveViewSelectedIndex(bool is_going_up)
        {
            // check if anything is selected

            if (WindowListView.Items.Count == 0)
            {
                if (WindowListView.SelectedItems.Count != 0)
                {
                    //WindowListView.SelectedIndices.Add(0);
                    WindowListView.Clear();
                }
            }
            else if (WindowListView.Items.Count > 0) {
                if (WindowListView.SelectedItems.Count == 0)
                {
                    WindowListView.SelectedIndices.Add(0);
                }
                if (!is_going_up)
                {
                    if (WindowListView.SelectedIndices[0] == WindowListView.Items.Count - 1)
                    {
                        WindowListView.SelectedIndices.Add(0);
                    }
                    else
                    {
                        if (hasChangedSelection)
                            WindowListView.SelectedIndices.Add(WindowListView.SelectedIndices[0] + 1);
                        else
                        {
                            WindowListView.SelectedIndices.Add(0);
                            hasChangedSelection = true;
                        }
                    }
                }
                else
                {
                    if (WindowListView.SelectedIndices[0] == 0)
                    {
                        WindowListView.SelectedIndices.Add(WindowListView.Items.Count - 1);
                    }
                    else
                    {
                        WindowListView.SelectedIndices.Add(WindowListView.SelectedIndices[0] - 1);
                    }
                }
            }
            WindowListView.EnsureVisible(WindowListView.SelectedItems[0].Index);
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
            ResetWindowView();
            if (SearchTextBox.Text == "")
            {
                ResetWindowView();
            }
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
            // get hieght of listvie witem
            int itemHeight = 0;
            if (WindowListView.Items.Count > 0)
            {
                itemHeight = WindowListView.GetItemRect(0).Height;
            }
            WindowListView.Size = new Size(WindowListView.Size.Width, WindowListView.Items.Count * itemHeight + itemHeight);
            var height = SearchTextBox.Size.Height + WindowListView.Size.Height ;
            WindowListView.MaximumSize = new Size(this.Size.Width, this.MaximumSize.Height - 50);
            this.Size = new Size(this.Size.Width, height);
        }

        private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ResetWindowView()
        {
            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            // loop over all windows
            WindowListView.Items.Clear();
            foreach (KeyValuePair<IntPtr, string> window in windows)
            {
                // if the window name matches the text in the combo box
                // perform regex search on the window name
                // convert SearchTextBox.text to regex
                // escape special characters
                string pattern = Regex.Escape(SearchTextBox.Text);

                if (Regex.IsMatch(window.Value, pattern, RegexOptions.IgnoreCase))
                {

                    //WindowListView.Items.ContainsKey

                    if (!WindowListView.Items.ContainsKey(window.Value))
                    {
                        // add the window to the listview
                        WindowListView.Items.Add(window.Value);
                        
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        static extern uint GetClassLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        static extern IntPtr GetClassLong64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 64 bit version maybe loses significant 64-bit specific information
        /// </summary>
        static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return new IntPtr((long)GetClassLong32(hWnd, nIndex));
            else
                return GetClassLong64(hWnd, nIndex);
        }


        

        public static Image GetSmallWindowIcon(IntPtr hWnd)
        {
            uint WM_GETICON = 0x007f;
            IntPtr ICON_SMALL2 = new IntPtr(2);
            IntPtr IDI_APPLICATION = new IntPtr(0x7F00);
            int GCL_HICON = -14;
            try
            {
                IntPtr hIcon = default(IntPtr);

                hIcon = SendMessage(hWnd, WM_GETICON, ICON_SMALL2, IntPtr.Zero);

                if (hIcon == IntPtr.Zero)
                    hIcon = GetClassLongPtr(hWnd, GCL_HICON);

                if (hIcon == IntPtr.Zero)
                    hIcon = LoadIcon(IntPtr.Zero, (IntPtr)0x7F00/*IDI_APPLICATION*/);

                if (hIcon != IntPtr.Zero)
                    return new Bitmap(Icon.FromHandle(hIcon).ToBitmap(), 16, 16);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
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

                //WindowListBox.Items.Clear();
                //Dictionary<IntPtr, string> w = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
                //foreach (KeyValuePair<IntPtr, string> window in w)
                //{
                //    WindowListBox.Items.Add(window.Value);
                //}
                //SearchTextBox.Focus();
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            // check if ctrl+o is pressed
            if (e.Control && e.KeyCode == Keys.O)
            {
                // show the options form
                Options options = new Options();
                options.Show();
                e.SuppressKeyPress = true;
            }
        }

        private void WindoListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowListView.SelectedItems.Count == 0)
            {
                return;
            }
            string window_name;
            if (WindowListView.SelectedItems[0].ToString() == "")
            {
                return;
            }

            window_name = WindowListView.SelectedItems[0].Text;


            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            foreach (KeyValuePair<IntPtr, string> window in windows)
            {
                Debug.WriteLine(window.Value + " + " + window_name);
                if (window.Value == window_name)
                {
                    FocusWindow(window.Key);
                    Hide();
                    return;
                }
            }
        }

        private void WindowListView_SelectedIndexChanged(object sender, EventArgs e)
        {

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

