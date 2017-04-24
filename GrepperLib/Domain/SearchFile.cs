using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GrepperLib.Model;
using GrepperLib.Utility;

namespace GrepperLib.Domain
{
    public enum SearchType
    {
        RegEx,
        GeneralMatch,
        ExactMatch
    }

    public class SearchFile : ISearchFile
    {
        private List<FileData> _fileData;
        private RegexOptions _regOptions;
        Func<Search, string, bool> IsSearchFunc;

        public SearchFile()
        {
            _fileData = new List<FileData>();
        }

        public IList<FileData> Search(Search search)
        {
            if (search == null)
                throw new ArgumentNullException();

            return FileSearch(search);
        }

        private SearchType GetSearchType(Search search)
        {
            if (search.DoMatchPhrase)
                return SearchType.ExactMatch;
            else if (search.IsLiteralSearch)
                return SearchType.GeneralMatch;
            else
                return SearchType.RegEx;
        }

        private IList<FileData> FileSearch(Search search)
        {
            _fileData = new List<FileData>();
            if (!search.DoMatchCase) search.SearchTerm = search.SearchTerm.ToLower();
            SearchOption so = search.IsRecursiveSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            foreach (var extension in search.FileExtensions.FileExtensionList)
            {
                var files = Directory.EnumerateFiles(search.BaseSearchPath, extension, so).Where(x => x.Contains('\\'));
                AddFileDataForExtension(search, files, extension);
            }

            return _fileData;
        }

        private void AddFileDataForExtension(Search search, IEnumerable<string> files, string extension)
        {
            foreach (var file in files)
            {
                var fileData = GetFileData(search, file, extension);
                if (fileData.IsValid())
                    _fileData.Add(fileData);
            }
        }

        private FileData GetFileData(Search search, string file, string extension)
        {
            var fileData = new FileData(file.Substring(file.LastIndexOf('\\') + 1), file.Trim(), extension);
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                string originalLine; // saves the letter casing
                uint lineNumber = 1;

                _regOptions = (search.DoMatchCase) ? RegexOptions.None : RegexOptions.IgnoreCase;

                SearchType searchType = GetSearchType(search);
                IsSearchFunc = FindByLiteral;
                if (searchType == SearchType.RegEx)
                    IsSearchFunc = FindByRegEx;
                if (searchType == SearchType.ExactMatch)
                    IsSearchFunc = FindByExact;

                while ((line = sr.ReadLine()) != null)
                {
                    originalLine = line;
                    if (!search.DoMatchCase)
                        line = line.ToLower();

                    if (IsSearchFunc.Invoke(search, line))
                        fileData.SetLineData(lineNumber, originalLine);

                    lineNumber++;
                }
            }
            
            return fileData;
        }

        private bool FindByRegEx(Search search, string line)
        {
            bool isFound = false;

            try
            {
                isFound = Regex.IsMatch(line, search.SearchTerm, _regOptions);
            }
            catch (Exception) 
            {
                Message.Add("Invalid search term.");
            }

            return isFound;
        }

        private bool FindByLiteral(Search search, string line)
        {
            bool isFound = false;
            string phrase = string.Format(@"(\b)({0}+(\b|\n|\s))", search.SearchTerm);
            try
            {
                isFound = Regex.IsMatch(line, phrase, _regOptions);
            }
            catch (Exception)
            {
                Message.Add("Invalid search term.");
            }

            return isFound;
        }

        private bool FindByExact(Search search, string line)
        {
            return line.Contains(search.SearchTerm);
        }
    }
}
