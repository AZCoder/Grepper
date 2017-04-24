namespace GrepperView
{
    partial class ExtensionsUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSavedExtensions = new System.Windows.Forms.Label();
            this.lbExtensions = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lbRegisteredTypes = new System.Windows.Forms.ListView();
            this.colExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopyLeft = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(848, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // lblSavedExtensions
            // 
            this.lblSavedExtensions.AutoSize = true;
            this.lblSavedExtensions.Location = new System.Drawing.Point(9, 16);
            this.lblSavedExtensions.Name = "lblSavedExtensions";
            this.lblSavedExtensions.Size = new System.Drawing.Size(92, 13);
            this.lblSavedExtensions.TabIndex = 1;
            this.lblSavedExtensions.Text = "Saved Extensions";
            // 
            // lbExtensions
            // 
            this.lbExtensions.FormattingEnabled = true;
            this.lbExtensions.Location = new System.Drawing.Point(12, 32);
            this.lbExtensions.Name = "lbExtensions";
            this.lbExtensions.Size = new System.Drawing.Size(473, 316);
            this.lbExtensions.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(12, 360);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete Item";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // lbRegisteredTypes
            // 
            this.lbRegisteredTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colExtension,
            this.colType});
            this.lbRegisteredTypes.Location = new System.Drawing.Point(492, 32);
            this.lbRegisteredTypes.Name = "lbRegisteredTypes";
            this.lbRegisteredTypes.Size = new System.Drawing.Size(432, 316);
            this.lbRegisteredTypes.TabIndex = 4;
            this.lbRegisteredTypes.UseCompatibleStateImageBehavior = false;
            this.lbRegisteredTypes.View = System.Windows.Forms.View.Details;
            // 
            // colExtension
            // 
            this.colExtension.Tag = "";
            this.colExtension.Text = "Extension";
            this.colExtension.Width = 126;
            // 
            // colType
            // 
            this.colType.Tag = "";
            this.colType.Text = "Media Type";
            this.colType.Width = 310;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(491, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Available Registered Types";
            // 
            // btnCopyLeft
            // 
            this.btnCopyLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyLeft.Location = new System.Drawing.Point(492, 360);
            this.btnCopyLeft.Name = "btnCopyLeft";
            this.btnCopyLeft.Size = new System.Drawing.Size(92, 23);
            this.btnCopyLeft.TabIndex = 6;
            this.btnCopyLeft.Text = "Copy To Left";
            this.btnCopyLeft.UseVisualStyleBackColor = true;
            this.btnCopyLeft.Click += new System.EventHandler(this.CopyLeft_Click);
            // 
            // ExtensionsUI
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(936, 395);
            this.ControlBox = false;
            this.Controls.Add(this.btnCopyLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbRegisteredTypes);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lbExtensions);
            this.Controls.Add(this.lblSavedExtensions);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtensionsUI";
            this.Opacity = 0.9D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extensions Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSavedExtensions;
        private System.Windows.Forms.ListBox lbExtensions;
        protected System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListView lbRegisteredTypes;
        private System.Windows.Forms.ColumnHeader colExtension;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopyLeft;
    }
}