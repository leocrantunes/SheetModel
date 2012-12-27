/**
 * OclAssocEndDeriveConstraint object instance interface.
 */

namespace Ocl20.library.iface.constraints
{
    public interface OclAssocEndDeriveConstraint : OclDeriveConstraint {
        /**
     * Returns the value of reference derivedAssocEnd.
     * @return Value of reference derivedAssocEnd.
     */
        common.CoreAssociationEnd getDerivedAssocEnd();
        /**
     * Sets the value of reference derivedAssocEnd. See {@link #getDerivedAssocEnd} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setDerivedAssocEnd(common.CoreAssociationEnd newValue);
    }
}
