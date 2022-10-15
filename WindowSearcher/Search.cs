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
        private IntPtr desktopHandle; //Window handle for the desktop
        private IntPtr shellHandle; //Window handle for the shell
        public Search()
        {
            InitializeComponent();

            int id = 0;
            uint keyModifier = (int)KeyModifier.Alt;
            uint key = (uint)Keys.F1.GetHashCode();

            desktopHandle = GetDesktopWindow();
            shellHandle = GetShellWindow();


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
                addWindowToDataGrid(window);
            }
            WindowDataGridView.CurrentCell.Selected = false;

            Resize();
        }

        public void addWindowToDataGrid(KeyValuePair<IntPtr, string> w)
        {
            WindowDataGridView.Rows.Add(GetSmallWindowIcon(w.Key), w.Value);
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
                        MoveViewSelectedIndexDataGrid(true);
                        break;

                    case Keys.Down:
                        MoveViewSelectedIndexDataGrid(false);
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
                            ResetDataGridView();
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
                                SearchTextBox.Text = "";
                                Options o = new Options();
                                o.ShowDialog();
                            } else if(SearchTextBox.Text.Trim().Equals("/exit"))
                            {
                                SearchTextBox.Text = "";
                                Application.Exit();
                                break;
                            } else
                            {
                                MessageBox.Show("Command not found");
                            }
                        }

                        // find handle for text
                        foreach (KeyValuePair<IntPtr, string> window in WindowList)
                        {
                            if (WindowDataGridView.SelectedRows.Count == 0)
                            {
                                break;
                            }
                            
                            if (window.Value.Equals(WindowDataGridView.SelectedRows[0].Cells[1].Value))
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
        
        public void MoveViewSelectedIndexDataGrid(bool is_going_up)
        {
            // check if anything is selected
            if(WindowDataGridView.Rows.Count == 0)
            {
                if (WindowDataGridView.SelectedCells.Count != 0)
                {
                    WindowDataGridView.ClearSelection();
                }
            } else if (WindowDataGridView.Rows.Count > 0)
            {
                if (WindowDataGridView.SelectedCells.Count == 0)
                {
                    WindowDataGridView.Rows[0].Selected = true;
                }

                if (!is_going_up)
                {
                    // see if we are at the end of the list
                    if (WindowDataGridView.SelectedRows[0].Index == WindowDataGridView.Rows.Count - 1)
                    {
                        Debug.WriteLine("Detected end of list");
                        WindowDataGridView.Rows[0].Selected = true;
                    } else
                    {
                        if(hasChangedSelection)
                        {
                            WindowDataGridView.Rows[WindowDataGridView.SelectedRows[0].Index + 1].Selected = true;
                        } else
                        {
                            WindowDataGridView.Rows[0].Selected = true;
                            hasChangedSelection = true;
                        }
                    }
                } else
                {
                    if (WindowDataGridView.SelectedRows[0].Index == 0)
                    {
                        WindowDataGridView.Rows[WindowDataGridView.Rows.Count - 1].Selected = true;
                    } else
                    {
                        WindowDataGridView.Rows[WindowDataGridView.SelectedRows[0].Index - 1].Selected = true;
                    }
                }
            }
            WindowDataGridView.FirstDisplayedScrollingRowIndex = WindowDataGridView.SelectedRows[0].Index;
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
            ResetDataGridView();
            //if (SearchTextBox.Text == "")
            //{
            //    ResetWindowView();
            //    ResetDataGridView();
            //}
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
            if (WindowDataGridView.Rows.Count > 0)
            {
                itemHeight = WindowDataGridView.RowTemplate.Height;
            }
            WindowDataGridView.Size = new Size(WindowDataGridView.Size.Width, WindowDataGridView.Rows.Count * itemHeight + itemHeight);
            var height = SearchTextBox.Size.Height + WindowDataGridView.Size.Height ;
            WindowDataGridView.MaximumSize = new Size(this.Size.Width, this.MaximumSize.Height - 50);
            this.Size = new Size(this.Size.Width, height);
        }

        private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ResetDataGridView()
        {
            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();
            // loop over all windows
            WindowDataGridView.Rows.Clear();

            foreach (KeyValuePair<IntPtr, string> window in windows)
            {
                // if the window name matches the text in the combo box
                // perform regex search on the window name
                // convert SearchTextBox.text to regex
                // escape special characters
                string pattern = Regex.Escape(SearchTextBox.Text);

                if (Regex.IsMatch(window.Value, pattern, RegexOptions.IgnoreCase))
                {
                    Debug.WriteLine("Passed regex: " +  window.Value);
                    // check if datagridview contains window.Value
                    bool hasFound = false;
                    foreach (DataGridViewRow row in WindowDataGridView.Rows)
                    {
                        if (row.Cells.Count == 2)
                        {
                            string cellValue = row.Cells[1].Value.ToString();
                            Debug.WriteLine("Cell Value: " + cellValue.ToString());
                            if (cellValue != null && cellValue.Contains(window.Value))
                            {
                                hasFound = true;
                            }
                        }
                    }
                    
                    if (!hasFound)
                    {
                        var image = GetSmallWindowIcon(window.Key);
                        WindowDataGridView.Rows.Add(image, window.Value.ToString());
                        Debug.WriteLine("Added " + window.Value.ToString());
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
                    return new Bitmap(System.Drawing.Icon.FromHandle(hIcon).ToBitmap(), 16, 16);
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

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowRect(IntPtr hwnd, out RECT rc);


        private int WM_HOTKEY = 0x0312;
        protected override void WndProc(ref Message hotkey)
        {
            base.WndProc(ref hotkey);

            if (hotkey.Msg == WM_HOTKEY)
            {
                // if fullscreen check is on
                if ((bool)Properties.Settings.Default["ConsiderFullScreen"] == true)
                {
                    //bool runningFullScreen = false;
                    RECT appBounds;
                    Rectangle screenBounds;
                    IntPtr hWnd;

                    // check if user is in a fullscreen application
                    hWnd = GetForegroundWindow();
                    if (!hWnd.Equals(IntPtr.Zero))
                    {
                        //Check we haven't picked up the desktop or the shell
                        if (!(hWnd.Equals(desktopHandle) || hWnd.Equals(shellHandle)))
                        {
                            GetWindowRect(hWnd, out appBounds);
                            //determine if window is fullscreen
                            screenBounds = Screen.FromHandle(hWnd).Bounds;
                            if ((appBounds.Bottom - appBounds.Top) == screenBounds.Height && (appBounds.Right - appBounds.Left) == screenBounds.Width)
                            {
                                return;
                            }
                        }
                    }
                }

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

