using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrepperLib.Utility
{
    public static class StringHelper
    {
        /// <summary>
        /// Converts a list of white-space or comma-delimited string values into a list of string types.
        /// </summary>
        /// <param name="fileExtensions">string</param>
        /// <returns>List<typeparamref name="string"/></returns>
        public static IList<string> ConvertStringToList(string stringList)
        {
            List<string> extensionList = new List<string>();
            if (stringList.Length <= 0) return extensionList;

            char[] delimeters = new char[] { ' ', ',' };
            string[] extensions = stringList.Split(delimeters);

            foreach (string word in extensions)
            {
                extensionList.Add(word.Trim());
            }

            return extensionList;
        }
    }
}
