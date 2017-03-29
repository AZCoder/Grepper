using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrepperLib.Utility
{
    /// <summary>
    /// Represents a single list of extensions that can be searched.
    /// The app can store multiple lists, but can only search a single list at a given time.
    /// </summary>
    public class FileExtension
    {
        private List<string> _fileExtensions;

        public IList<string> FileExtensionList
        {
            get
            {
                return _fileExtensions;
            }
        }

        public int Count
        {
            get
            {
                return _fileExtensions.Count;
            }
        }

        public FileExtension()
        {
            _fileExtensions = new List<string>();
        }

        public void Add(string extension)
        {
            _fileExtensions.Add(extension);
        }

        public void Remove(string extension)
        {
            _fileExtensions.Remove(extension);
        }

        public IList<string> LoadListFromSpacedString(string spacedString)
        {
            if (string.IsNullOrEmpty(spacedString))
                return null;

            char[] delimeters = new char[] { ' ', ',' };
            string[] extensions = spacedString.Split(delimeters);
            _fileExtensions = new List<string>();

            foreach (string word in extensions)
            {
                _fileExtensions.Add(word.Trim());
            }

            return _fileExtensions;
        }
    }
}
