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
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileExtension { get; set; }

        /// <summary>
        /// List of line numbers and the corresponding matching text for that line contained within this file.
        /// </summary>
        public IDictionary<long, string> LineDataList
        {
            get;
            protected set;
        }

        public FileData() { }

        public bool IsValid()
        {
            return (LineDataList != null && LineDataList.Count > 0);
        }

        public FileData(string fileName, string filePath, string fileExtension)
        {
            FileName = fileName;
            FilePath = filePath;
            FileExtension = fileExtension;
        }

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
    }
}