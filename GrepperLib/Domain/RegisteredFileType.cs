using System;
using System.Linq;
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
            List<FileType> fileTypes = new List<FileType>();
            for (int i = 0; i < types.Length; i++)
            {
                if (string.IsNullOrEmpty(types[i]))
                    continue;

                if (types[i].IndexOf(".") != 0)
                    continue;

                RegistryKey key = OpenKey(types[i]);
                if (key == null)
                    continue;

                var val = key.GetValue("Content Type");
                if (val != null)
                    fileTypes.Add(new FileType { ContentType = val.ToString(), Extension = types[i] });
            }

            return fileTypes;
        }

        private RegistryKey OpenKey(string path)
        {
            RegistryKey key = null;
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
