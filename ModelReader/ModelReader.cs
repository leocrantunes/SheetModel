using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Ocl20.library.iface.common;

namespace ModelReader
{
    public abstract class ModelReader
    {
        public abstract CoreModel getModel(XNamespace xnamespace);
    }
}
