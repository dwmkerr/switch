using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SwitchCore.Options
{
    public partial class ExtensionSwitchForm : Form
    {
        public ExtensionSwitchForm()
        {
            InitializeComponent();
        }

        private void ExtensionSwitchForm_Load(object sender, EventArgs e)
        {
            textBoxFrom.Text = From;
            textBoxTo.Text = To;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            From = textBoxFrom.Text;
            To = textBoxTo.Text;
        }

        public string From { get; set; }

        public string To { get; set; }
    }
}
