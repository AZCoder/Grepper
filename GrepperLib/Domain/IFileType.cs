﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public interface IRegisteredFileType
    {
        IList<FileType> GetRegisteredFileTypes();
    }
}