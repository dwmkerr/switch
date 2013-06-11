using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwitchCore.Configuration;

namespace SwitchCore.Options
{
    public partial class OptionsPage : UserControl
    {
        public OptionsPage()
        {
            InitializeComponent();

            //  Load the model.
            FromModel(SwitchAddin.Instance.Configuration);
        }

        private void SaveChanges()
        {
            ToModel(SwitchAddin.Instance.Configuration);
            SwitchAddin.Instance.SaveConfiguration();
        }

        public void FromModel(Configuration.SwitchConfiguration model)
        {
            checkBoxEnableSwitchDesigner.Checked = model.EnableSwitchBetweenDesignerAndCodeBehind;
            checkBoxEnableSwitchInterface.Checked = model.EnableSwitchBetweenInterfaceAndImplementation;
            
            listViewExtensionSwitches.Items.Clear();
            model.ExtensionSwitches.ForEach(es => listViewExtensionSwitches.Items.Add(new ListViewItem(new[] {es.From, es.To})));
        }

        public void ToModel(Configuration.SwitchConfiguration model)
        {

            model.EnableSwitchBetweenDesignerAndCodeBehind = checkBoxEnableSwitchDesigner.Checked;
            model.EnableSwitchBetweenInterfaceAndImplementation = checkBoxEnableSwitchInterface.Checked;

            model.ExtensionSwitches.Clear();
            listViewExtensionSwitches.Items.OfType<ListViewItem>().ToList().ForEach(
                lvi => model.ExtensionSwitches.Add(new ExtensionSwitch(lvi.SubItems[0].Text, lvi.SubItems[1].Text)));
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //  Create the new item form.
            var form = new ExtensionSwitchForm() {Text = "New Extension Switch"};
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                listViewExtensionSwitches.Items.Add(new ListViewItem(new[] {form.From, form.To}));
                SaveChanges();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //  Get the first selected item.
            var item = listViewExtensionSwitches.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            if (item != null)
            {
                //  Create the new item form.
                var form = new ExtensionSwitchForm() { Text = "Edit Extension Switch", From = item.SubItems[0].Text, To = item.SubItems[1].Text};
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    item.SubItems[0].Text = form.From;
                    item.SubItems[1].Text = form.To;
                    SaveChanges();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var item = listViewExtensionSwitches.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            if (item != null)
            {
                listViewExtensionSwitches.Items.Remove(item);
                SaveChanges();
            }
        }

        private void listViewExtensionSwitches_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEdit.Enabled = listViewExtensionSwitches.SelectedIndices.Count > 0;
            buttonDelete.Enabled = listViewExtensionSwitches.SelectedIndices.Count > 0;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            //  Get confirmation.
            if(MessageBox.Show(this, "Are you sure you want to reset to the default settings?", "Are you sure?", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;

            //  Reset to default.
            SwitchAddin.Instance.CreateDefaultConfiguration();
            SwitchAddin.Instance.SaveConfiguration();
            FromModel(SwitchAddin.Instance.Configuration);
        }
    }
}
