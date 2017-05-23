using System.Security;
using Microsoft.Win32;

namespace ContextMenu
{
    public static class ContextMenu
    {
        private static readonly string _grepperPath = "Folder\\Shell\\Grepper\\";
        private static readonly string _commandPath = _grepperPath + "Command";

        /// <summary>
        /// Adds the right-click context menu to Explorer
        /// </summary>
        /// <param name="path">File system path to the executable</param>
        public static void AddContextMenu(string path)
        {
            // Key Exists?
            RegistryKey key = GetKey(_grepperPath);
            if (key == null)
                Registry.ClassesRoot.CreateSubKey(_grepperPath);

            key = GetKey(_commandPath) ?? Registry.ClassesRoot.CreateSubKey(_commandPath);
            if (key == null)
                return;

            object keyData = key.GetValue("");
            if ((keyData != null) && (keyData.ToString().Length >= 1)) return;
            path = $@"""{path}"" -p""%1""";
            key.SetValue("", path);
        }
        
        private static RegistryKey GetKey(string path)
        {
            RegistryKey key;

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
