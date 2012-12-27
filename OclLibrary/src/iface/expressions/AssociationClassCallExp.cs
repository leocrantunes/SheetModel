/**
 * AssociationClassCallExp object instance interface.
 */

using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface AssociationClassCallExp
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
