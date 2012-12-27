/**
 * TuplePartType object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.types
{
    public interface TuplePartType : CoreAttribute {
        /**
     * Returns the value of reference tupleType.
     * @return Value of reference tupleType.
     */
        TupleType getTupleType();
        /**
     * Sets the value of reference tupleType. See {@link #getTupleType} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setTupleType(TupleType newValue);
    }
}
