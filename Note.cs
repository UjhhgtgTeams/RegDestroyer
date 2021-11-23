using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUpdateTool
{
    public partial class Note : Form
    {
        public Note()
        {
            InitializeComponent();
        }

        private void Note_Load(object sender, EventArgs e)
        {
            noteBox.Text = "YOUR COMPUTER HAS BEEN TRASHED BY THE REGDESTROYER TROJAN!!! NOW ENJOY YOUR BROKEN PC!!!";
        }

        private void Note_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
