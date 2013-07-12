using System;
using System.Collections.Generic;

namespace GrepperLib.Model
{
    /// <summary>
    /// Holds a list of one or more LineData objects
    /// representing a single file.
    /// </summary>
    public class FileData
    {
        #region Public Properties______

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileExtension { get; set; }

        /// <summary>
        /// Read-only property for the LineDataList. Data
        /// must be set through the separate method with the line
        /// number and data both provided.
        /// </summary>
        public IDictionary<long, string> LineDataList
        {
            get;
            protected set;
        }

        #endregion
        #region Constructors___________

        public FileData() { }

        public FileData(string fileName, string filePath, string fileExtension)
        {
            FileName = fileName;
            FilePath = filePath;
            FileExtension = fileExtension;
        }

        #endregion
        #region Public Methods_________
        
        /// <summary>
        /// Adds a line of data for this file object consisting of both the
        /// line number and the representative data.
        /// </summary>
        /// <param name="lineNumber">long</param>
        /// <param name="lineData">string</param>
        public void SetLineData(long lineNumber, string lineData)
        {
            if (LineDataList == null) LineDataList = new Dictionary<long, string>();
            LineDataList.Add(lineNumber, lineData.Trim());
        }
        
        #endregion
    }
}