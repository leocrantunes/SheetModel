/**
 * AttributeCallExp object instance interface.
 */

using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface AttributeCallExp : ModelPropertyCallExp {
        /**
     * Returns the value of reference referredAttribute.
     * @return Value of reference referredAttribute.
     */
        CoreAttribute getReferredAttribute();
        /**
     * Sets the value of reference referredAttribute. See {@link #getReferredAttribute} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setReferredAttribute(CoreAttribute newValue);
    }
}
