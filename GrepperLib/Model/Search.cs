﻿using System.Text.RegularExpressions;
using GrepperLib.Utility;

namespace GrepperLib.Model
{
    public class Search
    {
        private string _baseSearchPath;

        public string SearchTerm { get; set; }
        public string BaseSearchPath
        {
            get
            {
                return _baseSearchPath;
            }
            set
            {
                SetSearchPath(value);
            }
        }

        private void SetSearchPath(string value)
        {
            _baseSearchPath = string.Empty;
            // match a drive letter pattern only
            var reg = new Regex("^[a-zA-Z][:]{1}");
            if (reg.IsMatch(value))
                _baseSearchPath = value;
        }

        public FileExtension FileExtensions { get; set; }
        public bool IsLiteralSearch { get; set; }
        public bool IsRecursiveSearch { get; set; }
        public bool DoMatchPhrase { get; set; }
        public bool DoMatchCase { get; set; }

        public Search()
        {
            FileExtensions = new FileExtension();
        }
    }
}
