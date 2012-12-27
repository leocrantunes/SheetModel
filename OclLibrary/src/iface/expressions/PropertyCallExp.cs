/**
 * PropertyCallExp object instance interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface PropertyCallExp : OclExpression {
        /**
     * Returns the value of reference source.
     * @return Value of reference source.
     */
        OclExpression getSource();
        /**
     * Sets the value of reference source. See {@link #getSource} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setSource(OclExpression newValue);
    }
}
