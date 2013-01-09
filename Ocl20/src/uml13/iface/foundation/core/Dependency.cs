/**
 * Dependency object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Dependency : Relationship {
        /**
     * Returns the value of reference client.
     * @return Value of reference client.
     */
       List<object> getClient();
        /**
     * Returns the value of reference supplier.
     * @return Value of reference supplier.
     */
       List<object> getSupplier();
    }
}
