/**
 * CoreStereotype object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreStereotype : CoreModelElement {
        /**
     * Returns the value of reference theExtendedElement.
     * @return Value of reference theExtendedElement.
     */
        List<object> getTheExtendedElement();
    }
}
