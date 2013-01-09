/**
 * Binding object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Binding : Dependency {
        /**
     * Returns the value of reference argument.
     * @return Value of reference argument.
     */
        List<object> getArgument();
    }
}
