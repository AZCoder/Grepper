using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;

namespace Grepper.ContextMenu
{
    public static class RegistrySettings
    {
        private static string _grepperPath = "Folder\\Shell\\Grepper\\";
        private static string _commandPath = _grepperPath + "Command";
        private static string _settingsPath = _grepperPath + "Settings";
        private static string _extensionItemPath = _grepperPath + "Settings\\ExtensionItems";
        private static string _searchItemPath = _grepperPath + "Settings\\SearchItems";

        /// <summary>
        /// Adds the right-click context menu to explorer with the Grepper option.
        /// </summary>
        /// <param name="path">File system path to grepper.exe</param>
        public static void AddContextMenu(string path)
        {
            // Key Exists?
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_grepperPath, true);
            if (key == null) key = Registry.ClassesRoot.CreateSubKey(_grepperPath);
            key = Registry.ClassesRoot.OpenSubKey(_commandPath, true);
            if (key == null) key = Registry.ClassesRoot.CreateSubKey(_commandPath);
            object keyData = key.GetValue("");
            if ((keyData == null) || (keyData.ToString().Length < 1))
            {
                path = string.Format(@"""{0}"" -p""%1""", path);
                key.SetValue("", path);
            }
        }

        /// <summary>
        /// Returns the last extension list used.
        /// </summary>
        /// <returns>String of extensions.</returns>
        public static string GetLastExtension()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_settingsPath, true);
            if (key == null) key = AddSettingsKeys();
            string lastExtension = LoadSettingString(GrepperKeyName.extensionItem);
            if (!string.IsNullOrEmpty(lastExtension))
            {
                key = Registry.ClassesRoot.OpenSubKey(_extensionItemPath, true);
                if (key != null)
                {
                    var item = from v in key.GetValueNames()
                               where v == lastExtension.Trim()
                               select key.GetValue(v).ToString();

                    if (item != null && item.Count() > 0)
                        lastExtension = item.First();
                }
            }

            return lastExtension;
        }

        /// <summary>
        /// Returns string list of all extensions that have been saved. This allows
        /// different groupings of extensions to be saved.
        /// </summary>
        /// <returns>String List of extensions.</returns>
        public static IList<string> LoadExtensions()
        {
            List<string> extensionValues = new List<string>();
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_extensionItemPath, true);
            if (key == null) key = Registry.ClassesRoot.CreateSubKey(_extensionItemPath);
            foreach (string keyName in key.GetValueNames())
            {
                if (key.GetValue(keyName) != null)
                    extensionValues.Add(key.GetValue(keyName).ToString());
                else
                    extensionValues.Add(string.Empty);
            }

            return extensionValues;
        }

        public static IList<string> LoadSearchItems()
        {
            List<string> searchItems = new List<string>();
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_searchItemPath, true);
            if (key == null) key = Registry.ClassesRoot.CreateSubKey(_searchItemPath);
            foreach (string keyName in key.GetValueNames())
            {
                if (key.GetValue(keyName) != null)
                    searchItems.Add(key.GetValue(keyName).ToString());
                else
                    searchItems.Add(string.Empty);
            }

            return searchItems;
        }

        /// <summary>
        /// Returns the setting for the given key name.
        /// </summary>
        /// <param name="keyName">Enum of the possible key names.</param>
        /// <returns>String value of the registry key.</returns>
        public static string LoadSettingString(GrepperKeyName keyName)
        {
            // Key Exists?
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_settingsPath, true);
            if (key == null) key = AddSettingsKeys();

            if (key.GetValue(keyName.ToString()) == null) AddSettingsSubKey(key, keyName, RegistryValueKind.String, string.Empty);
            return (key.GetValue(keyName.ToString()) != null) ? key.GetValue(keyName.ToString()).ToString() : string.Empty;
        }

        /// <summary>
        /// Returns the setting for the given key name.
        /// </summary>
        /// <param name="keyName">Enum of the possible key names.</param>
        /// <returns>Boolean value of the registry key.</returns>
        public static bool LoadSettingBool(GrepperKeyName keyName)
        {
            // Key Exists?
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_settingsPath, true);
            if (key == null) AddSettingsKeys();

            byte[] isChecked = (byte[])key.GetValue(keyName.ToString());
            if (isChecked == null) AddSettingsSubKey(key, keyName, RegistryValueKind.Binary, "0");
            isChecked = (byte[])key.GetValue(keyName.ToString());
            return (isChecked != null && isChecked[0] == 1) ? true : false;
        }

        /// <summary>
        /// Saves a registry STRING subkey value to the registry.
        /// </summary>
        /// <param name="keyName">Key name of the setting.</param>
        /// <param name="keyValue">Value to save.</param>
        public static void SaveSettingString(GrepperKeyName keyName, string keyValue)
        {
            // Key Exists?
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_settingsPath, true);
            if (key != null)
            {
                AddSettingsSubKey(key, keyName, RegistryValueKind.String, keyValue);
            }
        }

        /// <summary>
        /// Saves a registry BYTE subkey value to the registry.
        /// </summary>
        /// <param name="keyName">Key name of the setting.</param>
        /// <param name="keyValue">Value to save.</param>
        public static void SaveSettingBool(GrepperKeyName keyName, bool keyValue)
        {
            // Key Exists?
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_settingsPath, true);
            if (key != null)
            {
                string val = (keyValue) ? "1" : "0";
                AddSettingsSubKey(key, keyName, RegistryValueKind.Binary, val);
            }
        }

        /// <summary>
        /// Saves a registry STRING subkey value to the registry.
        /// </summary>
        /// <param name="itemName">List of extension items to save.</param>
        public static void SaveExtensionItems(IList<string> itemList)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_extensionItemPath, true);
            if (key != null && itemList.Count > 0)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    key.SetValue(string.Format("extension{0}", i + 1), itemList[i].ToString());
                }
            }
        }

        /// <summary>
        /// Saves a list of registry STRING subkey value to the registry.
        /// Limits itself to 5 items.
        /// </summary>
        /// <param name="itemName">List of search items to save.</param>
        public static void SaveSearchItems(IList<string> itemList)
        {
            DeleteSearchItems();
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_searchItemPath, true);
            int items = itemList.Count > 5 ? 5 : itemList.Count;
            if (key != null && itemList.Count > 0)
            {
                for (int i = 0; i < items; i++)
                {
                    key.SetValue(string.Format("search{0}", i + 1), itemList[i].ToString());
                }
            }
        }

        /// <summary>
        /// Removes all search items so they can be rebuilt.
        /// </summary>
        /// <returns></returns>
        public static void DeleteSearchItems()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_searchItemPath, true);

            List<string> searchList = LoadSearchItems().ToList();
            for (int i = 0; i < searchList.Count; i++)
            {
                key.DeleteValue(string.Format("search{0}", i + 1));
            }
        }

        /// <summary>
        /// Removes an extension item registry key if its value matches the parameter.
        /// </summary>
        /// <param name="item"></param>
        public static bool DeleteExtensionItem(string item)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_extensionItemPath, true);
            if (key == null && item.Length <= 0) return false;

            // remove all extensions, then re-add them except for the deleted item
            // this will maintain numerical order
            List<string> extensionList = LoadExtensions().ToList();
            extensionList.Remove(item.Trim());

            // overwrite existing key/values
            SaveExtensionItems(extensionList);

            // remove exceess key (should be one)
            int extension = extensionList.Count();
            key.DeleteValue(string.Format("extension{0}", extension + 1));

            // set the current extension key
            SaveSettingString(GrepperKeyName.extensionItem, "extension1");
            return true;
        }

        /// <summary>
        /// Sets 'extensionItem' in Settings to the currently used extensions.
        /// </summary>
        /// <param name="extension">The string of extension names (not the key, just the values)</param>
        public static void SaveCurrentExtension(string extension)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_extensionItemPath, true);
            if (key == null) key = Registry.ClassesRoot.CreateSubKey(_extensionItemPath);

            var item = from v in key.GetValueNames()
                       where key.GetValue(v.Trim()).ToString() == extension.Trim()
                       select v;

            if (item != null && item.Count() > 0)
                SaveSettingString(GrepperKeyName.extensionItem, item.First());
        }

        /// <summary>
        /// Adds the user settings key to registry.
        /// </summary>
        /// <returns>RegistryKey</returns>
        private static RegistryKey AddSettingsKeys()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(_settingsPath, true);
            if (key == null) key = Registry.ClassesRoot.CreateSubKey(_settingsPath);
            return key;
        }

        /// <summary>
        /// Adds settings subkey to registry.
        /// </summary>
        /// <param name="key">Parent key name.</param>
        /// <param name="keyName">Enum of registry subkey.</param>
        /// <param name="rvk">Kind of registry value (binary, string, etc).</param>
        /// <param name="keyValue">Optional value, will assume empty or 0 values if not provided.</param>
        private static void AddSettingsSubKey(RegistryKey key, GrepperKeyName keyName, RegistryValueKind rvk, string keyValue = "")
        {
            if (rvk == RegistryValueKind.String)
            {
                key.SetValue(keyName.ToString(), keyValue, RegistryValueKind.String);
            }
            if (rvk == RegistryValueKind.Binary)
            {
                byte[] bit = new byte[1];
                bit[0] = (keyValue == "1") ? (byte)1 : (byte)0;
                key.SetValue(keyName.ToString(), bit, RegistryValueKind.Binary);
            }
        }

        /// <summary>
        /// Keys stored in the registry for this application.
        /// </summary>
        public enum GrepperKeyName
        {
            matchCase,
            matchPhrase,
            recursive,
            literal,
            extensionItem,
            search
        }
    }
}