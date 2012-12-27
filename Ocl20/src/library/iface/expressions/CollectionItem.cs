/**
 * CollectionItem object instance interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface CollectionItem : CollectionLiteralPart {
        /**
     * Returns the value of reference item.
     * @return Value of reference item.
     */
        OclExpression getItem();
        /**
     * Sets the value of reference item. See {@link #getItem} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setItem(OclExpression newValue);
    }
}
