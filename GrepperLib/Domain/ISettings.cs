using System.Collections.Generic;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public interface ISettings
    {
        bool SaveSettings(Settings settings);
        Settings LoadSettings();
        IList<string> GetExtensions();
        bool SaveExtensions(IList<string> extensions);
        bool SaveSearches(IList<string> searches);
        bool DeleteExtension(string extension);
    }
}
