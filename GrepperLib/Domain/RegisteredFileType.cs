using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Win32;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public class RegisteredFileType : IRegisteredFileType
    {
        public IList<FileType> GetRegisteredFileTypes()
        {
            var fileTypes = LoadFileTypes();
            return fileTypes;
        }

        private IList<FileType> LoadFileTypes()
        {
            var types = Registry.ClassesRoot.GetSubKeyNames();
            var fileTypes = new List<FileType>();
            foreach (string t in types)
            {
                if (string.IsNullOrEmpty(t))
                    continue;

                if (t.IndexOf(".", StringComparison.Ordinal) != 0)
                    continue;

                var key = OpenKey(t);

                var val = key?.GetValue("Content Type");
                if (val != null)
                    fileTypes.Add(new FileType { ContentType = val.ToString(), Extension = t });
            }

            return fileTypes;
        }

        private RegistryKey OpenKey(string path)
        {
            RegistryKey key;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(path);
            }
            catch (SecurityException)
            {
                key = null;
            }

            return key;
        }
    }
}
