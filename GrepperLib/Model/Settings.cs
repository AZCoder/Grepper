using System.Collections.Generic;

namespace GrepperLib.Model
{
    public class Settings
    {
        public bool IsLiteral { get; set; }
        public bool MatchCase { get; set; }
        public bool MatchPhrase { get; set; }
        public bool IsRecursive { get; set; }
        public string SearchTerm { get; set; }
        public string LastExtension { get; set; }
        public IList<string> SavedExtensions { get; set; }
        public IList<string> SavedSearchTerms { get; set; }
    }
}
