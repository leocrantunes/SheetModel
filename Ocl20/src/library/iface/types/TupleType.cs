/**
 * TupleType object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.util;

namespace Ocl20.library.iface.types
{
    public interface TupleType : CoreDataType {
        /**
     * Returns the value of reference factory.
     * @return Value of reference factory.
     */
        AstOclModelElementFactory getFactory();
        /**
     * Sets the value of reference factory. See {@link #getFactory} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setFactory(AstOclModelElementFactory newValue);
        /**
     * Returns the value of reference tupleParts.
     * @return Value of reference tupleParts.
     */
        List<object> getTupleParts();
    }
}
