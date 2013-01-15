using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ocl20.library.iface.util;

namespace Ocl20.library.impl.util
{
    public class UtilPackageImpl : UtilPackage
    {
        public AstOclModelElementFactory getAstOclModelElementFactory()
        {
            return new AstOclModelElementFactoryImpl();
        }
    }
}
