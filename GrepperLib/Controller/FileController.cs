using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GrepperLib.Model;
using GrepperLib.Utility;

namespace GrepperLib.Controller
{
    public class FileController
    {
        #region Private Members________

        private string _baseSearchPath;
        private IList<FileData> _fileDataList;
        private IList<string> _fileExtensionList;

        #endregion Private Members
        #region Public Properties______

        public string BaseSearchPath
        {
            get
            {
                return _baseSearchPath;
            }
            set
            {
                // match a drive letter pattern only
                Regex reg = new Regex("^[a-zA-Z][:]{1}");
                if (reg.IsMatch(value))
                    _baseSearchPath = value;
                else
                    _baseSearchPath = string.Empty;
            }
        }

        public IList<FileData> FileDataList
        {
            get
            {
                return _fileDataList;
            }
        }

        public string FileExtensions { get; set; }

        public bool RecursiveSearch { get; set; }

        public bool MatchCase { get; set; }

        public bool MatchPhrase { get; set; }

        public bool LiteralSearch { get; set; }
        
        public string SearchCriteria { get; set; }

        public int TotalMatches
        {
            get
            {
                if (FileDataList == null)
                    return 0;
                else
                {
                    int count = 0;
                    foreach (FileData fd in FileDataList)
                    {
                        count += fd.LineDataList.Count;
                    }
                    return count;
                }
            }
        }

        #endregion
        #region Constructor____________

        public FileController()
        {
            _baseSearchPath = string.Empty;
            FileExtensions = string.Empty;
            RecursiveSearch = true;
            MatchCase = false;
            MatchPhrase = false;
            Message.Clear();
            _fileDataList = new List<FileData>();
        }

        #endregion Constructor
        #region Public Methods_________

        /// <summary>
        /// Creates a list of FileData objects and
        /// </summary>
        /// <param name="criteria">criteria to search</param>
        public void GenerateFileData()
        {
            Message.Clear();

            // if no criteria or no file extensions or no base path, there is no way data can be generated
            if (string.IsNullOrEmpty(SearchCriteria)) Message.Add("No search criteria provided.");
            if (string.IsNullOrEmpty(FileExtensions)) Message.Add("No file extensions provided.");
            if (string.IsNullOrEmpty(BaseSearchPath)) Message.Add("No search path provided.");
            if (Message.MessageList.Count > 0) return;

            _fileDataList = new List<FileData>();
            _fileExtensionList = new List<string>();
            _fileExtensionList = StringHelper.ConvertStringToList(FileExtensions);

            if (!MatchCase) SearchCriteria = SearchCriteria.ToLower();
            foreach (string extension in _fileExtensionList)
            {
                try
                {
                    SearchFiles(extension);
                }
                catch (PathTooLongException ptle)
                {
                    Message.Add(ptle.Message);
                }
                catch (IOException ioe)
                {
                    Message.Add(ioe.Message);
                }
                catch (UnauthorizedAccessException uax)
                {
                    Message.Add(uax.Message);
                }
            }
        }

        #endregion Public Methods
        #region Private Methods________

        private void SearchFiles(string extension)
        {
            SearchOption so = RecursiveSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = from file in Directory.EnumerateFiles(BaseSearchPath, extension, so)
                        select file;

            foreach (var f in files)
            {
                if (!f.Contains('\\')) continue;
                FileData fileData = new FileData()
                {
                    FileExtension = extension,
                    FileName = f.Substring(f.LastIndexOf('\\') + 1),
                    FilePath = f.Trim()
                };

                using (StreamReader sr = new StreamReader(f))
                {
                    string line;
                    string originalLine; // saves the letter casing
                    uint lineNumber = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        originalLine = line;
                        if (!MatchCase) line = line.ToLower();

                        if (LiteralSearch)
                        {
                            if (MatchPhrase)
                            {
                                // criteria to find search pattern that ignores certain boundaries
                                string phrase = string.Format(@"(\b)({0}+(\b|\n|\s))", SearchCriteria);

                                RegexOptions regOptions = (MatchCase) ? RegexOptions.None : RegexOptions.IgnoreCase;
                                if (Regex.IsMatch(line, phrase, regOptions))
                                {
                                    fileData.SetLineData(lineNumber, originalLine);
                                }
                            }
                            else
                            {
                                if (line.Contains(SearchCriteria))
                                {
                                    fileData.SetLineData(lineNumber, originalLine);
                                }
                            }
                        }
                        else
                        {
                            // pattern treated as REGEX
                            RegexOptions regOptions = (MatchCase) ? RegexOptions.None : RegexOptions.IgnoreCase;
                            if (Regex.IsMatch(line, SearchCriteria, regOptions))
                            {
                                fileData.SetLineData(lineNumber, originalLine);
                            }
                        }

                        lineNumber++;
                    }
                }
                if (fileData.LineDataList != null && fileData.LineDataList.Count > 0) _fileDataList.Add(fileData);
            }
        }

        #endregion Private Methods
    }
}