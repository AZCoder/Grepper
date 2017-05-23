using System.Collections.Generic;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public interface ISearchFile
    {
        IList<FileData> Search(Search search);
    }
}
