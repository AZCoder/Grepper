using System.Collections.Generic;

namespace GrepperLib.Domain
{
    public interface IFileDirectory
    {
        string GetPathAtLoadup(IList<string> arguments);
    }
}
