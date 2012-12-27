/**
 * OclAssocEndInitConstraint object instance interface.
 */

namespace OclLibrary.iface.constraints
{
    public interface OclAssocEndInitConstraint : OclInitConstraint {
        /**
     * Returns the value of reference initializedAssocEnd.
     * @return Value of reference initializedAssocEnd.
     */
        common.CoreAssociationEnd getInitializedAssocEnd();
        /**
     * Sets the value of reference initializedAssocEnd. See {@link #getInitializedAssocEnd} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setInitializedAssocEnd(common.CoreAssociationEnd newValue);
    }
}
