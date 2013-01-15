using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreStereotypeImpl : CoreModelElementImpl, CoreStereotype
    {
        private List<object> extendedElement;

        public CoreStereotypeImpl()
        {
            extendedElement = new List<object>();
        }

        public List<object> getExtendedElement()
        {
            return extendedElement;
        }

        public void setExtendedElement(List<object> newValue)
        {
            extendedElement = newValue;
        }
    }
}
