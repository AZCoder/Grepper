using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GrepperLib.Model;
using GrepperLib.Utility;

namespace GrepperLib.Domain
{
    public class SearchFile : ISearchFile
    {
        private List<FileData> _fileData;
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

        private IList<FileData> FileSearch(Search search)
        {
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

        public delegate bool IsSearchSuccessful(Search search, string line);

        private FileData GetFileData(Search search, string file, string extension)
        {
            var fileData = new FileData(file.Substring(file.LastIndexOf('\\') + 1), file.Trim(), extension);
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                string originalLine; // saves the letter casing
                uint lineNumber = 1;
                IsSearchSuccessful isSearch;

                while ((line = sr.ReadLine()) != null)
                {
                    originalLine = line;
                    if (!search.DoMatchCase) line = line.ToLower();

                    if (search.IsLiteralSearch)
                    {
                        if (search.DoMatchPhrase)
                        {
                            // criteria to find search pattern that ignores certain boundaries
                            string phrase = string.Format(@"(\b)({0}+(\b|\n|\s))", search.SearchTerm);

                            RegexOptions regOptions = (search.DoMatchCase) ? RegexOptions.None : RegexOptions.IgnoreCase;
                            if (Regex.IsMatch(line, phrase, regOptions))
                            {
                                fileData.SetLineData(lineNumber, originalLine);
                            }
                        }
                        else
                        {
                            if (line.Contains(search.SearchTerm))
                            {
                                fileData.SetLineData(lineNumber, originalLine);
                            }
                        }
                    }
                    else
                    {
                        isSearch = new IsSearchSuccessful(FindByRegEx);
                        var b = isSearch(search, line);

                        // pattern treated as REGEX
                        RegexOptions regOptions = (search.DoMatchCase) ? RegexOptions.None : RegexOptions.IgnoreCase;
                        if (Regex.IsMatch(line, search.SearchTerm, regOptions))
                        {
                            fileData.SetLineData(lineNumber, originalLine);
                        }
                    }

                    lineNumber++;
                }
            }


            return fileData;
        }

        private bool FindByRegEx(Search search, string line)
        {
            RegexOptions regOptions = (search.DoMatchCase) ? RegexOptions.None : RegexOptions.IgnoreCase;
            return Regex.IsMatch(line, search.SearchTerm, regOptions);
        }
    }
}
