/**
 * TupleType object instance interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.util;

namespace OclLibrary.iface.types
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
