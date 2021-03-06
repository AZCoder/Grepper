﻿using System.Collections.Generic;
using System.Linq;

namespace GrepperLib.Utility
{
    /// <summary>
    /// Represents a single list of extensions that can be searched.
    /// The app can store multiple lists, but can only search a single list at a given time.
    /// </summary>
    public class FileExtension
    {
        private List<string> _fileExtensions;

        public IList<string> FileExtensionList => _fileExtensions;

        public int Count => _fileExtensions.Count;

        public FileExtension()
        {
            _fileExtensions = new List<string>();
        }

        public void Add(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return;

            // can only add single extension (not a spaced list)
            extension = extension.Substring(0, extension.IndexOf(' '));
            _fileExtensions.Add(extension);
        }

        public void Remove(string extension)
        {
            _fileExtensions?.Remove(extension);
        }

        public string GetSpacedStringFromList(IList<string> extList = null)
        {
            string spacedString = string.Empty;
            if (extList == null)
                extList = _fileExtensions;

            spacedString = extList.Aggregate(spacedString, (current, ext) => current + (ext + " "));
            return spacedString.TrimEnd();
        }
        
        public IList<string> MergeListWithSpacedString(IList<string> baseList, string spacedString)
        {
            // merge list stored in _fileExtensions with provided parameter
            if (string.IsNullOrEmpty(spacedString))
                return null;

            if (baseList == null)
                baseList = new List<string>();

            var parmList = ConvertSpacedStringToList(spacedString);
            baseList = baseList.Union(parmList).ToList();

            return baseList;
        }

        public IList<string> LoadListFromSpacedString(string spacedString)
        {
            var results = ConvertSpacedStringToList(spacedString);
            if (results == null)
                return null;

            _fileExtensions = results.ToList();
            return _fileExtensions;
        }

        public IList<string> ConvertSpacedStringToList(string spacedString)
        {
            if (string.IsNullOrEmpty(spacedString))
                return null;

            var delimeters = new[] { ' ', ',' };
            var extensions = spacedString.Split(delimeters);

            return extensions.Select(word => word.Trim()).ToList();
        }
    }
}
