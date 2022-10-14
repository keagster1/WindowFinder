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
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk); //handle, Id of hotkey, modifier (e.g ALT + DEL), hotkey key
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static void Init(ComboBox comboBox)
        {

        }

        public static void SetHotKey(IntPtr hWnd, uint modifiers, uint keyHashCode)
        {
            UnregisterHotKey(hWnd, 0);
            RegisterHotKey(hWnd, 0, modifiers, keyHashCode);
        }
    }
}
