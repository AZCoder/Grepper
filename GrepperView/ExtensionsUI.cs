using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GrepperLib.Domain;
using GrepperLib.Utility;

namespace GrepperView
{
    public partial class ExtensionsUI : Form
    {
        protected readonly ISettings _settings;

        public ExtensionsUI()
        {
            _settings = new SettingsManager();
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

            if (_settings.DeleteExtension(lbExtensions.SelectedItem.ToString()))
            {
                lbExtensions.Items.Clear();
                LoadExtensions();
            }
        }

        private void ViewAvailableExtensions()
        {
            var rft = new RegisteredFileType();
            var result = rft.GetRegisteredFileTypes();
            lbRegisteredTypes.Items.Clear();
            foreach (var item in result)
            {
                var lvi = new ListViewItem(new[] { item.Extension, item.ContentType });
                lbRegisteredTypes.Items.Add(lvi);
            }

            lbRegisteredTypes.CheckBoxes = true;
        }

        private IList<string> GetCheckedItems()
        {
            var checkedItems = lbRegisteredTypes.CheckedItems;
            if (checkedItems.Count < 1)
                return null;

            var checkedList = new List<string>();
            for (var i = 0; i < checkedItems.Count; i++)
            {
                checkedList.Add(checkedItems[i].Text);
            }

            return checkedList;
        }

        private void LoadExtensions()
        {
            lbExtensions.Items.Clear();
            foreach (var item in _settings.GetExtensions())
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

            var extension = new FileExtension();
            IList<string> baseList = new List<string>();
            var allExtensions = _settings.GetExtensions();
            var selectedItem = lbExtensions.SelectedItem?.ToString();
            // if existing saved item is selected on left, then modify that row, otherwise add new row of checked items
            if (!string.IsNullOrEmpty(selectedItem))
            {
                baseList = extension.ConvertSpacedStringToList(selectedItem);
                allExtensions.Remove(selectedItem);
            }

            var checkedList = new List<string>();
            foreach (var item in checkedItems)
            {
                checkedList.Add(item);
            }

            var rowItem = extension.GetSpacedStringFromList(checkedList);
            baseList = extension.MergeListWithSpacedString(baseList, rowItem);
            var spacedItem = extension.GetSpacedStringFromList(baseList);
            allExtensions.Add(spacedItem);
            _settings.SaveExtensions(allExtensions);
            LoadExtensions();
        }
    }
}
