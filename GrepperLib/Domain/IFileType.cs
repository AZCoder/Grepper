using System.Collections.Generic;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public interface IRegisteredFileType
    {
        IList<FileType> GetRegisteredFileTypes();
    }
}
