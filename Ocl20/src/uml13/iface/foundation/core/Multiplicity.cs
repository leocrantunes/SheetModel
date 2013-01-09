/**
 * Multiplicity object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Multiplicity {
        /**
     * Returns the value of reference range.
     * @return Value of reference range.
     */
        List<object> getRange();
    }
}
