/**
 * AssociationClassCallExp object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.expressions
{
    public interface AssociationClassCallExp : NavigationCallExp
    {
        /**
         * Returns the value of reference referredAssociationClass.
         * @return Value of reference referredAssociationClass.
         */
        CoreAssociationClass getReferredAssociationClass();
        /**
         * Sets the value of reference referredAssociationClass. See {@link #getReferredAssociationClass} 
         * for description on the reference.
         * @param newValue New value to be set.
         */
        void setReferredAssociationClass(CoreAssociationClass newValue);
    }
}
