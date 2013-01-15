/**
 * CoreStereotype object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreStereotype : CoreModelElement {

        List<object> getExtendedElement();
        void setExtendedElement(List<object> newValue);
    }
}
