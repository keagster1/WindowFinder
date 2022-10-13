using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowSearcher
{
    internal class HotKeys
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk); //handle, Id of hotkey, modifier (e.g ALT + DEL), hotkey key
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static void Init(ComboBox comboBox)
        {

        }

        public static void SetHotKey(IntPtr hWnd)
        {
            UnregisterHotKey(hWnd, 0);
            //int mod = 0;
            //if (Control.ModifierKeys == Keys.Control)
            //    mod = 2;
            //else if (Control.ModifierKeys == Keys.Alt)
            //    mod = 1;
            //else if (Control.ModifierKeys == Keys.Shift)
            //    mod = 4;
            //Debug.WriteLine("Hotkey: " + mod + " " + (int)Control.MouseButtons);
            RegisterHotKey(hWnd, 0, 1, Keys.F1.GetHashCode());
        }
    }
}
