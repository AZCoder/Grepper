using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace GrepperLib.Domain
{
    public class FileDirectory : IFileDirectory
    {
        protected static readonly string _cleanRootPathPattern = "^[a-zA-Z][:]{1}";

        public static string GetRootPathPattern => _cleanRootPathPattern;

        public string GetPathAtLoadup(IList<string> arguments)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!arguments.Any())
                return path;

            string argPath = arguments.First();
            if (argPath.StartsWith("-p"))
                path = argPath.Substring(2);

            path = CleanRootPath(path);
            return path;
        }

        protected string CleanRootPath(string path)
        {
            var regPattern = new Regex(_cleanRootPathPattern);
            if (regPattern.Matches(path.Substring(0, 2)).Count < 1)
            {
                return path;
            }

            if (path.Substring(2, 1) == "\"")
            {
                path = $"{path.Substring(0, 2)}\\";
            }

            return path;
        }
    }
}
