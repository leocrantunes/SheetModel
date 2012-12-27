/**
 * CoreStereotype object instance interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OclLibrary.iface.common
{
    public interface CoreStereotype : CoreModelElement {
        /**
     * Returns the value of reference theExtendedElement.
     * @return Value of reference theExtendedElement.
     */
        List<object> getTheExtendedElement();
    }
}
