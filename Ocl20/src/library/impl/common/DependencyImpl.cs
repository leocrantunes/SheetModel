using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class DependencyImpl : CoreModelElementImpl,  Dependency
    {
        private List<object> client;
        private List<object> supplier;

        public DependencyImpl()
        {
            client = new List<object>();
            supplier = new List<object>();
        }

        public List<object> getClient()
        {
            return client;
        }

        public void setClient(List<object> newValue)
        {
            client = newValue;
        }

        public List<object> getSupplier()
        {
            return supplier;
        }

        public void setSupplier(List<object> newValue)
        {
            supplier = newValue;
        }
    }
}
