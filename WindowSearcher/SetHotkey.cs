using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowSearcher
{
    public partial class SetHotkey : Form
    {
        public SetHotkey()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SetHotkey_Load(object sender, EventArgs e)
        {

        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            // If the user pressed a key, display its value in the label.
            if (m.Msg == 0x100)
            {
                Keys keyData = (Keys)m.WParam;
                if (keyData == Keys.Escape)
                {
                    this.Close();
                }
            }
            return base.ProcessKeyPreview(ref m);
        }
    }
}
