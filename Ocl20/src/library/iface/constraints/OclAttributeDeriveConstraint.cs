/**
 * OclAttributeDeriveConstraint object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface OclAttributeDeriveConstraint : OclDeriveConstraint {
        /**
     * Returns the value of reference derivedAttribute.
     * @return Value of reference derivedAttribute.
     */
        CoreAttribute getDerivedAttribute();
        /**
     * Sets the value of reference derivedAttribute. See {@link #getDerivedAttribute} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setDerivedAttribute(CoreAttribute newValue);
    }
}
