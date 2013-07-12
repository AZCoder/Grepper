using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grepper.ContextMenu;

namespace GrepperView
{
    public partial class ExtensionsUI : Form
    {
        public ExtensionsUI()
        {
            InitializeComponent();
            this.LoadExtensions();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbExtensions.SelectedItem != null)
            {
                if (RegistrySettings.DeleteExtensionItem(lbExtensions.SelectedItem.ToString()))
                {
                    // update UI
                    lbExtensions.Items.Clear();
                    LoadExtensions();
                }
            }
        }

        /// <summary>
        /// Loads extensions saved in the registry.
        /// </summary>
        private void LoadExtensions()
        {
            foreach (string item in RegistrySettings.LoadExtensions())
            {
                lbExtensions.Items.Add(item);
            }
        }
    }
}
