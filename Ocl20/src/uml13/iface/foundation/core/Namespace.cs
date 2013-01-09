/**
 * Namespace object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Namespace : ModelElement {
        /**
     * Returns the value of reference ownedElement.
     * @return Value of reference ownedElement.
     */
        List<object> getOwnedElement();
    }
}
