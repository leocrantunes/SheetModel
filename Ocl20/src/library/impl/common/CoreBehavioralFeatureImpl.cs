using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreBehavioralFeatureImpl : CoreFeatureImpl, CoreBehavioralFeature
    {
        public virtual bool isQuery() { throw new NotImplementedException(); }
        public virtual List<object> getParameter() { throw new NotImplementedException(); }
    }
}
