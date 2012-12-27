/**
 * OclAttributeDeriveConstraint object instance interface.
 */

using OclLibrary.iface.common;

namespace OclLibrary.iface.constraints
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
