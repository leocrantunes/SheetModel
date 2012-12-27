/**
 * AssociationEndCallExp object instance interface.
 */
namespace Ocl20.library.iface.expressions
{
    public interface AssociationEndCallExp : NavigationCallExp {
        /**
     * Returns the value of reference referredAssociationEnd.
     * @return Value of reference referredAssociationEnd.
     */
        common.CoreAssociationEnd getReferredAssociationEnd();
        /**
     * Sets the value of reference referredAssociationEnd. See {@link #getReferredAssociationEnd} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setReferredAssociationEnd(common.CoreAssociationEnd newValue);
    }
}
