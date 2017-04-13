using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrepperLib.Domain
{
    public interface IFileDirectory
    {
        string GetPathAtLoadup(IList<string> arguments);
    }
}
