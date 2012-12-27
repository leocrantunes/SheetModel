/**
 * OclAttributeInitConstraint object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface OclAttributeInitConstraint : OclInitConstraint {
        /**
     * Returns the value of reference initializedAttribute.
     * @return Value of reference initializedAttribute.
     */
        CoreAttribute getInitializedAttribute();
        /**
     * Sets the value of reference initializedAttribute. See {@link #getInitializedAttribute} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setInitializedAttribute(CoreAttribute newValue);
    }
}
