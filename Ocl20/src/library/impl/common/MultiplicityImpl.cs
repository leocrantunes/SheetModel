using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class MultiplicityImpl : Multiplicity
    {
        private List<object> range;

        public MultiplicityImpl()
        {
            range = new List<object>();
        }

        public List<object> getRange()
        {
            return range;
        }

        public void setRange(List<object> newValue)
        {
            range = newValue;
        }
    }
}
