namespace SwitchCore.Options
{
    partial class OptionsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableSwitchDesigner = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableSwitchInterface = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listViewExtensionSwitches = new System.Windows.Forms.ListView();
            this.columnHeaderFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxEnableSwitchDesigner);
            this.groupBox1.Controls.Add(this.checkBoxEnableSwitchInterface);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Settings";
            // 
            // checkBoxEnableSwitchDesigner
            // 
            this.checkBoxEnableSwitchDesigner.AutoSize = true;
            this.checkBoxEnableSwitchDesigner.Location = new System.Drawing.Point(18, 47);
            this.checkBoxEnableSwitchDesigner.Name = "checkBoxEnableSwitchDesigner";
            this.checkBoxEnableSwitchDesigner.Size = new System.Drawing.Size(268, 17);
            this.checkBoxEnableSwitchDesigner.TabIndex = 1;
            this.checkBoxEnableSwitchDesigner.Text = "Enable Switch between Designer and Code-Behind";
            this.checkBoxEnableSwitchDesigner.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableSwitchInterface
            // 
            this.checkBoxEnableSwitchInterface.AutoSize = true;
            this.checkBoxEnableSwitchInterface.Location = new System.Drawing.Point(18, 24);
            this.checkBoxEnableSwitchInterface.Name = "checkBoxEnableSwitchInterface";
            this.checkBoxEnableSwitchInterface.Size = new System.Drawing.Size(278, 17);
            this.checkBoxEnableSwitchInterface.TabIndex = 0;
            this.checkBoxEnableSwitchInterface.Text = "Enable Switch between Interface and Implementation";
            this.checkBoxEnableSwitchInterface.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonReset);
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonEdit);
            this.groupBox2.Controls.Add(this.buttonAdd);
            this.groupBox2.Controls.Add(this.listViewExtensionSwitches);
            this.groupBox2.Location = new System.Drawing.Point(3, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 225);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Switch Extensions";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(327, 77);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 0;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Location = new System.Drawing.Point(327, 48);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Edit...";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(327, 19);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add...";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // listViewExtensionSwitches
            // 
            this.listViewExtensionSwitches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFrom,
            this.columnHeaderTo});
            this.listViewExtensionSwitches.FullRowSelect = true;
            this.listViewExtensionSwitches.Location = new System.Drawing.Point(18, 19);
            this.listViewExtensionSwitches.Name = "listViewExtensionSwitches";
            this.listViewExtensionSwitches.Size = new System.Drawing.Size(303, 187);
            this.listViewExtensionSwitches.TabIndex = 1;
            this.listViewExtensionSwitches.UseCompatibleStateImageBehavior = false;
            this.listViewExtensionSwitches.View = System.Windows.Forms.View.Details;
            this.listViewExtensionSwitches.SelectedIndexChanged += new System.EventHandler(this.listViewExtensionSwitches_SelectedIndexChanged);
            // 
            // columnHeaderFrom
            // 
            this.columnHeaderFrom.Text = "From";
            this.columnHeaderFrom.Width = 120;
            // 
            // columnHeaderTo
            // 
            this.columnHeaderTo.Text = "To";
            this.columnHeaderTo.Width = 120;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(327, 183);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset...";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "OptionsPage";
            this.Size = new System.Drawing.Size(445, 325);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxEnableSwitchDesigner;
        private System.Windows.Forms.CheckBox checkBoxEnableSwitchInterface;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListView listViewExtensionSwitches;
        private System.Windows.Forms.ColumnHeader columnHeaderFrom;
        private System.Windows.Forms.ColumnHeader columnHeaderTo;
        private System.Windows.Forms.Button buttonReset;
    }
}
