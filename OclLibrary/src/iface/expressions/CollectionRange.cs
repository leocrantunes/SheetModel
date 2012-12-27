/**
 * CollectionRange object instance interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface CollectionRange : CollectionLiteralPart {
        /**
     * Returns the value of reference first.
     * @return Value of reference first.
     */
        OclExpression getFirst();
        /**
     * Sets the value of reference first. See {@link #getFirst} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setFirst(OclExpression newValue);
        /**
     * Returns the value of reference last.
     * @return Value of reference last.
     */
        OclExpression getLast();
        /**
     * Sets the value of reference last. See {@link #getLast} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setLast(OclExpression newValue);
    }
}
