using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grepper.ContextMenu;
using GrepperLib.Domain;
using GrepperLib.Utility;

namespace GrepperView
{
    public partial class ExtensionsUI : Form
    {
        public ExtensionsUI()
        {
            InitializeComponent();
            LoadExtensions();
        }

        protected void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (lbExtensions.SelectedItem == null)
                return;
            
            if (RegistrySettings.DeleteExtensionItem(lbExtensions.SelectedItem.ToString()))
            {
                // update UI
                lbExtensions.Items.Clear();
                LoadExtensions();
            }
        }

        private void ViewAvailableExtensions()
        {
            RegisteredFileType rft = new RegisteredFileType();
            var result = rft.GetRegisteredFileTypes();
            lbRegisteredTypes.Items.Clear();
            foreach (var item in result)
            {
                ListViewItem lvi = new ListViewItem(new string[] { item.Extension, item.ContentType });
                lbRegisteredTypes.Items.Add(lvi);
            }

            lbRegisteredTypes.CheckBoxes = true;
        }

        private IList<string> GetCheckedItems()
        {
            var checkedItems = lbRegisteredTypes.CheckedItems;
            if (checkedItems != null && checkedItems.Count < 1)
                return null;

            List<string> checkedList = new List<string>();
            for (int i = 0; i < checkedItems.Count; i++)
            {
                checkedList.Add(checkedItems[i].Text);
            }

            return checkedList;
        }

        /// <summary>
        /// Loads extensions saved in the registry.
        /// </summary>
        private void LoadExtensions()
        {
            lbExtensions.Items.Clear();
            foreach (string item in RegistrySettings.LoadExtensions())
            {
                lbExtensions.Items.Add(item);
            }

            ViewAvailableExtensions();
        }

        private void CopyLeft_Click(object sender, EventArgs e)
        {
            var checkedItems = GetCheckedItems();
            if (checkedItems == null)
                return;

            FileExtension extension = new FileExtension();
            IList<string> baseList = new List<string>();
            string selectedItem = lbExtensions.SelectedItem.ToString();
            // if existing saved item is selected on left, then modify that row, otherwise add new row of checked items
            if (!string.IsNullOrEmpty(selectedItem))
                baseList = extension.ConvertSpacedStringToList(selectedItem);

            List<string> checkedList = new List<string>();
            foreach (var item in checkedItems)
            {
                checkedList.Add(item);
            }

            string rowItem = extension.GetSpacedStringFromList(checkedList);
            baseList = extension.MergeListWithSpacedString(baseList, rowItem);
            string spacedItem = extension.GetSpacedStringFromList(baseList);
            var allExtensions = RegistrySettings.LoadExtensions();
            allExtensions.Add(spacedItem);
            RegistrySettings.SaveExtensionItems(allExtensions, selectedItem);
            LoadExtensions();
        }
    }
}
