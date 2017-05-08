using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace Grepper.ContextMenu
{
    public static class ContextMenu
    {
        private static string _grepperPath = "Folder\\Shell\\Grepper\\";
        private static string _commandPath = _grepperPath + "Command";

        /// <summary>
        /// Adds the right-click context menu to Explorer
        /// </summary>
        /// <param name="path">File system path to the executable</param>
        public static void AddContextMenu(string path)
        {
            // Key Exists?
            RegistryKey key = GetKey(_grepperPath);
            if (key == null)
                key = Registry.ClassesRoot.CreateSubKey(_grepperPath);

            key = GetKey(_commandPath);
            if (key == null)
                key = Registry.ClassesRoot.CreateSubKey(_commandPath);

            object keyData = key.GetValue("");
            if ((keyData == null) || (keyData.ToString().Length < 1))
            {
                path = string.Format(@"""{0}"" -p""%1""", path);
                key.SetValue("", path);
            }
        }
        
        private static RegistryKey GetKey(string path)
        {
            RegistryKey key = null;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(path, true);
            }
            catch (SecurityException)
            {
                key = null;
            }

            return key;
        }
    }
}
