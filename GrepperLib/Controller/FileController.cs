using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrepperLib.Model;
using GrepperLib.Utility;
using GrepperLib.Domain;

namespace GrepperLib.Controller
{
    public class FileController
    {
        protected const string NOSEARCHCRITERIA = "No search criteria provided.";
        protected const string NOFILEEXTENSIONS = "No file extensions provided.";
        protected const string NOSEARCHPATH = "No search path provided.";

        protected readonly ISearchFile _searchFile;
        private Search _search;
        private IList<FileData> _fileDataList;

        public IList<FileData> FileDataList
        {
            get
            {
                return _fileDataList;
            }
        }

        public bool RecursiveSearch
        {
            get
            {
                return _search.IsRecursiveSearch;
            }
            set
            {
                _search.IsRecursiveSearch = value;
            }
        }

        public bool IsMatchCase
        {
            get
            {
                return _search.DoMatchCase;
            }
            set
            {
                _search.DoMatchCase = value;
            }
        }

        public bool DoMatchPhrase
        {
            get
            {
                return _search.DoMatchPhrase;
            }
            set
            {
                _search.DoMatchPhrase = value;
            }
        }

        public bool IsLiteralSearch
        {
            get
            {
                return _search.IsLiteralSearch;
            }
            set
            {
                _search.IsLiteralSearch = value;
            }
        }

        public string SearchCriteria
        {
            get
            {
                return _search.SearchTerm;
            }
            set
            {
                _search.SearchTerm = value;
            }
        }

        public int TotalMatches
        {
            get
            {
                if (FileDataList == null)
                    return 0;

                int count = 0;
                for (int i = 0; i < FileDataList.Count; i++)
                {
                    count += FileDataList[i].LineDataList.Count;
                }

                return count;
            }
        }

        public FileController()
        {
            _search = new Search();
            _searchFile = new SearchFile();
            _fileDataList = new List<FileData>();
        }

        public void SetBaseSearchPath(string path)
        {
            _search.BaseSearchPath = path;
        }

        public void LoadFileExtensionsFromString(string extensions)
        {
            _search.FileExtensions.LoadListFromSpacedString(extensions);
        }

        /// <summary>
        /// Creates a list of FileData objects and
        /// </summary>
        /// <param name="criteria">criteria to search</param>
        public void GenerateFileData()
        {
            // if no criteria or no file extensions or no base path, there is no way data can be generated
            ValidateFormCriteria();
            if (Message.MessageList.Count > 0)
                return;

            try
            {
                _fileDataList = _searchFile.Search(_search);
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
            catch (ArgumentNullException ane)
            {
                Message.Add(ane.Message);
            }
        }

        private void ValidateFormCriteria()
        {
            Message.Clear();
            if (string.IsNullOrEmpty(SearchCriteria)) Message.Add(NOSEARCHCRITERIA);
            if (_search.FileExtensions.Count < 1) Message.Add(NOFILEEXTENSIONS);
            if (string.IsNullOrEmpty(_search.BaseSearchPath)) Message.Add(NOSEARCHPATH);
        }
    }
}