using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.environment;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.iface.util;
using Ocl20.library.impl.common;
using Ocl20.library.impl.types;
using Ocl20.library.impl.util;

namespace Ocl20.library.impl
{
    public class Ocl20PackageImpl : Ocl20Package
    {
        public UtilPackage getUtil()
        {
            return new UtilPackageImpl();
        }

        public EnvironmentPackage getEnvironment()
        {
            throw new NotImplementedException();
        }

        public ConstraintsPackage getConstraints()
        {
            throw new NotImplementedException();
        }

        public ExpressionsPackage getExpressions()
        {
            throw new NotImplementedException();
        }

        public TypesPackage getTypes()
        {
            return new TypesPackageImpl();
        }

        public CommonPackage getCommon()
        {
            return new CommonPackageImpl();
        }
    }
}
