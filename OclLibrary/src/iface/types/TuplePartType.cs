/**
 * TuplePartType object instance interface.
 */

using OclLibrary.iface.common;

namespace OclLibrary.iface.types
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
