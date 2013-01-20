using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreBehavioralFeatureImpl : CoreFeatureImpl, CoreBehavioralFeature
    {
        private bool isQuery;
        private List<Parameter> parameter;

        public CoreBehavioralFeatureImpl()
        {
            parameter = new List<Parameter>();
        }

        public virtual bool getIsQuery()
        {
            return isQuery;
        }

        public void setIsQuery(bool newValue)
        {
            isQuery = newValue;
        }
        
        public virtual List<Parameter> getParameter()
        {
            return parameter;
        }

        public virtual void setParameter(List<Parameter> newValue)
        {
            parameter = newValue;
        }
    }
}
