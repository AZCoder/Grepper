using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public interface ISettings
    {
        bool SaveSettings(Settings settings);
        Settings LoadSettings();
        IList<string> LoadExtensions(); // TODO - technically part of Settings, but ExtensionsUI only needs this list
        bool DeleteExtension(string extension);
    }
}
