/**
 * Association object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface UmlAssociation : GeneralizableElement, Relationship {
        /**
     * Returns the value of reference connection.
     * @return Value of reference connection.
     */
        List<object> getConnection();
    }
}
