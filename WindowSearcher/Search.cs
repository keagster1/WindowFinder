using System.Runtime.InteropServices;
using System.Text;
using HWND = System.IntPtr;
using System.Text.RegularExpressions;

namespace WindowFinder
{
    public partial class Search : Form
    {
        // Used for detecting and setting the global hotkey
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        // used for full screen checking
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // !-- LOTS OF WINDOWS IMPORTS --!//
        // used when determining if the user is using a fullscreen application
        private readonly IntPtr desktopHandle; //Window handle for the desktop
        private readonly IntPtr shellHandle; //Window handle for the shell

        // Registers a hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        // Unregisters the hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool ShowWindowAsync(IntPtr windowHandle, int nCmdShow);
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        static extern uint GetClassLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        static extern IntPtr GetClassLong64(IntPtr hWnd, int nIndex);
        
        public delegate bool EnumWindowsProc(HWND hWnd, int lParam);
        
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL",CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowRect(IntPtr hwnd, out RECT rc);
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);
        // !-- END OF IMPORTS FINALLY --! //
        
        public Search()
        {
            InitializeComponent();

            // set up data for registering hotkey
            int id = 0;
            uint keyModifier = (int)KeyModifier.Alt;
            uint key = (uint)Keys.F1.GetHashCode();

            // these are used during full screen checks
            desktopHandle = GetDesktopWindow();
            shellHandle = GetShellWindow();


            // check if settings has values
            var settings = Properties.Settings.Default;

            // if settings has values, set the hotkey to the saved values
            if (settings.HotKey != 0 && settings.Modifiers != 0)
            {
                RegisterHotKey(this.Handle, id, (uint)Properties.Settings.Default["Modifiers"], (uint)Properties.Settings.Default["HotKey"]);
            }
            else
            {
                RegisterHotKey(this.Handle, id, keyModifier, key);

                // Save the default hotkey to settings
                Properties.Settings.Default["HotKey"] = key;
                Properties.Settings.Default["Modifiers"] = keyModifier;
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // immediatley set the max size to the forms size - the searchboxes hieght
            // this is done first because without it, the size will be wrong when the form is first shown
            WindowDataGridView.MaximumSize = new Size(WindowDataGridView.Size.Width, this.MaximumSize.Height - SearchTextBox.Size.Height);

            // Get the list of open windows and add them to the datagridview
            Dictionary<IntPtr, string> w = (Dictionary<HWND, string>)GetOpenWindows();
            foreach (KeyValuePair<IntPtr, string> window in w)
            {
                AddWindowToDataGrid(window);
            }
            
            // added this to fix issue where DataGrid selected the first item by default
            WindowDataGridView.CurrentCell.Selected = false;

            // change the size of the grid to fit the number of rows and 
            // change the form size to only show the textbox and grid
            ResizeListAndForm();
        }

        public void AddWindowToDataGrid(KeyValuePair<IntPtr, string> w)
        {
            // This is how we get the icon for the window
            // Fortunatley we can get it with the HWND which we have
            WindowDataGridView.Rows.Add(GetSmallWindowIcon(w.Key), w.Value);
        }

        // Overrided this to deal with navigating the datagridview with the arrow keys
        protected override bool ProcessKeyPreview(ref Message m)
        {
            // figure out the event
            const int WM_KEYDOWN = 0x0100;
            int msgVal = m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                // get the key that was pressed
                switch ((Keys)msgVal)
                {
                    // Fortunatley we can use the Keys library to simplify this
                    case Keys.Up:
                        MoveViewSelectedIndexDataGrid(true);
                        break;
                    case Keys.Down:
                        MoveViewSelectedIndexDataGrid(false);
                        break;
                    case Keys.Escape:
                        // if the user presses escape, close the form but only if that is the setting
                        if ((bool)Properties.Settings.Default["HideSearchWithEscape"])
                        {
                            this.Hide();
                            m.Result = (IntPtr)1;
                            return base.ProcessKeyPreview(ref m);
                        }
                        else
                        {
                            // Clear search box
                            SearchTextBox.Text = "";

                            // Clear the datagridview
                            ResetDataGridView();

                            // resize listbox to fit 10 items
                            ResizeListAndForm();
                            
                            // suppress the key event
                            m.Result = (IntPtr)1;
                            return base.ProcessKeyPreview(ref m);
                        }
                    case Keys.Enter:
                        // Check for commands
                        // TODO: Make commands show up in results list
                        if (SearchTextBox.Text.Trim().StartsWith("/"))
                        {
                            if (SearchTextBox.Text.Trim().Equals("/options"))
                            {
                                SearchTextBox.Text = "";
                                Options o = new();
                                o.ShowDialog();
                            }
                            else if (SearchTextBox.Text.Trim().Equals("/exit"))
                            {
                                SearchTextBox.Text = "";
                                HandleExit();
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Command not found");
                            }
                        }

                        // get list of open windows
                        Dictionary<IntPtr, string> WindowList = (Dictionary<HWND, string>)GetOpenWindows();

                        // loop over open windows
                        foreach (KeyValuePair<IntPtr, string> window in WindowList)
                        {
                            // If nothing is selected, don't do anything
                            if (WindowDataGridView.SelectedRows.Count == 0)
                            {
                                break;
                            }

                            // If the selected window name matches the open window
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

        private void ScrollGrid()
        {
            // Scrolling logic to keep the selected item in the middle of the list
            int halfWay = (WindowDataGridView.DisplayedRowCount(false) / 2);
            if (WindowDataGridView.FirstDisplayedScrollingRowIndex + halfWay > WindowDataGridView.SelectedRows[0].Index ||
                (WindowDataGridView.FirstDisplayedScrollingRowIndex + WindowDataGridView.DisplayedRowCount(false) - halfWay) <= WindowDataGridView.SelectedRows[0].Index)
            {
                int targetRow = WindowDataGridView.SelectedRows[0].Index;

                targetRow = Math.Max(targetRow - halfWay, 0);
                WindowDataGridView.FirstDisplayedScrollingRowIndex = targetRow;

            }
        }
        
        public bool hasChangedSelection = false;

        public void MoveViewSelectedIndexDataGrid(bool is_going_up)
        {
            // check if anything is selected
            if (WindowDataGridView.Rows.Count == 0)
            {
                if (WindowDataGridView.SelectedCells.Count != 0)
                {
                    WindowDataGridView.ClearSelection();
                }
            }
            else if (WindowDataGridView.Rows.Count > 0)
            {
                // make sure we have a selected item
                if (WindowDataGridView.SelectedCells.Count == 0)
                {
                    // Data grids have the Selected property attached to the row, not the grid component itself
                    WindowDataGridView.Rows[0].Selected = true;
                }

                // Slightly different logic to go up than down
                if (!is_going_up) // going down
                {
                    // see if we are at the end of the list
                    if (WindowDataGridView.SelectedRows[0].Index == WindowDataGridView.Rows.Count - 1)
                    {
                        // if we are, go to the top
                        // the grid is displayed from top to bottom with the top most being index 0
                        WindowDataGridView.Rows[0].Selected = true;
                    }
                    else
                    {
                        // otherwise, go down one
                        WindowDataGridView.Rows[WindowDataGridView.SelectedRows[0].Index + 1].Selected = true;
                    }
                }
                else
                {
                    // see if we are at the first row
                    if (WindowDataGridView.SelectedRows[0].Index == 0)
                    {
                        // go to the last row
                        WindowDataGridView.Rows[^1].Selected = true;
                    }
                    else
                    {
                        // go to the next row
                        WindowDataGridView.Rows[WindowDataGridView.SelectedRows[0].Index - 1].Selected = true;
                    }
                }
            }

            // Custom scroll code to center selected item
            ScrollGrid();
        }

        public static void FocusWindow(IntPtr hWnd)
        {
            // use the Handle of the window to go to
            // the window and forcibly show it.
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
                SetForegroundWindow(hWnd);
            }
        }

        // Not sure if I really needed to implement so many key handlers but here we are...
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            hasChangedSelection = false;
            ResetDataGridView();
            ResizeListAndForm();
        }

        public void ResizeListAndForm()
        {
            // If we have rows
            if (WindowDataGridView.Rows.Count > 0)
            {
                // Change the height to fit the size of all the rows combined
                WindowDataGridView.Height = WindowDataGridView.Rows.GetRowsHeight(DataGridViewElementStates.None);
            }
            else
            {
                // Set height to 0
                WindowDataGridView.Size = new Size(WindowDataGridView.Size.Width, 0);
            }

            // Update Search form height to show only search box and the new size of the datagrid
            var height = SearchTextBox.Size.Height + WindowDataGridView.Size.Height;
            this.Size = new Size(this.Size.Width, height + 1);
        }

        private void ResetDataGridView()
        {
            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)GetOpenWindows();
            
            // clear selection if possible
            if (WindowDataGridView.CurrentCell != null)
                WindowDataGridView.CurrentCell.Selected = false;
            
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
                    // check if datagridview contains window.Value by looping over 
                    // each row
                    bool hasFound = false;
                    foreach (DataGridViewRow row in WindowDataGridView.Rows)
                    {
                        if (row.Cells.Count == 2)
                        {
                            // attempt to get cell value and check if it equals the window name
                            string? cellValue = row.Cells[1].Value.ToString();
                            if (cellValue != null && cellValue.Contains(window.Value))
                            {
                                hasFound = true;
                            }
                        }
                    }

                    // Add the window
                    if (!hasFound)
                    {
                        var image = GetSmallWindowIcon(window.Key);
                        WindowDataGridView.Rows.Add(image, window.Value.ToString());
                    }
                }
            }
        }

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


        // This uses the Win API so we have to use bits in some cases
        public static Image? GetSmallWindowIcon(IntPtr hWnd)
        {
            // Icon related event codes
            uint WM_GETICON = 0x007f;
            IntPtr ICON_SMALL2 = new(2);
            int GCL_HICON = -14;
            try
            {
                IntPtr hIcon = default;

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

        // Special code to see if the user has the desktop active
        public static bool IsDesktopActive()
        {
            const int maxChars = 256;
            StringBuilder className = new(maxChars);

            int handle = (int)GetForegroundWindow();

            if (GetClassName(handle, className, maxChars) > 0)
            {
                string cName = className.ToString();
                if (cName == "Progman" || cName == "WorkerW")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // Yet another key handler only this time its being used for the global hotkey
        private readonly int WM_HOTKEY = 0x0312;
        protected override void WndProc(ref Message hotkey)
        {
            base.WndProc(ref hotkey);

            // we only care about the event for hotkeys
            if (hotkey.Msg == WM_HOTKEY)
            {
                // if fullscreen check is on
                if ((bool)Properties.Settings.Default["ConsiderFullScreen"] == true)
                {
                    Rectangle screenBounds;
                    IntPtr hWnd;

                    // check if user is in a fullscreen application
                    hWnd = GetForegroundWindow();
                    if (!hWnd.Equals(IntPtr.Zero))
                    {
                        //Check we haven't picked up the desktop or the shell
                        if (!IsDesktopActive() && (!(hWnd.Equals(desktopHandle) || hWnd.Equals(shellHandle))))
                        {
                            // Get rectangle of active window
                            // for GetWindowRect, it will return a non-zero number on success
                            var err = GetWindowRect(hWnd, out RECT appBounds);
                            if (err != 0)
                            {
                                //determine if window is fullscreen
                                screenBounds = Screen.FromHandle(hWnd).Bounds;

                                if ((appBounds.Bottom - appBounds.Top) == screenBounds.Height && (appBounds.Right - appBounds.Left) == screenBounds.Width)
                                {
                                    // We are in a fullscreen application so just return and suppress key press.
                                    hotkey.Result = (IntPtr)1;
                                    return;
                                }
                            }
                        }
                    }
                }

                // The code will only get here if the fullscreen check failed

                // Try to force our window to the front
                this.Show();
                this.WindowState = FormWindowState.Normal;
                Search.SetForegroundWindow(Handle);

                hotkey.Result = (IntPtr)1;
            }

            // Code to handle hiding the search when the application loses focus.
            const int WM_ACTIVATEAPP = 0x001C;
            if (hotkey.Msg == WM_ACTIVATEAPP)
            {
                if (hotkey.WParam.ToInt64() == 0)
                {
                    this.Hide();
                }
            }
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();

            // Should bring it to the front
            this.Activate();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleExit();
        }

        private void HandleExit()
        {
            // Unregister all hotkeys.
            UnregisterHotKey(this.Handle, 0);

            // Close notify
            WindowFinderNotify.Visible = false;

            // Finally exit
            Application.Exit();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new();
            options.Show();
        }

        // WE GOT ANOTHA ONE
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // added this to fix bug where
            // there would be a windows sound when pressing escape
            // this is added in the KeyDown even because it comes after the other key events higher in the code
            // which lets Escape do its function and then get suppressed.
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
                Options options = new();
                options.Show();
                e.SuppressKeyPress = true;
            }
        }
        private void WindowDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            // window name could technically be null
            string? window_name;

            // don't do anything if nothing is selected
            if (WindowDataGridView.SelectedRows[0].Cells[1].ToString() == "")
            {
                return;
            }

            // don't do anything if we can't get a windowname from the selected 
            window_name = WindowDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (window_name == null)
            {
                return;
            }

            // get open windows
            Dictionary<IntPtr, string> windows = (Dictionary<HWND, string>)GetOpenWindows();

            // find the Open window that matches the selected item
            foreach (KeyValuePair<IntPtr, string> window in windows)
            {
                if (window.Value == window_name)
                {
                    // Finally focus the window
                    FocusWindow(window.Key);
                    Hide();
                    return;
                }
            }
        }

        // Get the open windows from the system process list
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = Search.GetShellWindow();
            Dictionary<HWND, string> windows = new();

            Search.EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!Search.IsWindowVisible(hWnd)) return true;

                int length = Search.GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new(length);
                _ = GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;
            }, 0);

            return windows;
        }
    }
}