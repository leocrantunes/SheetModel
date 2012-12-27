/**
 * CoreAssociationClass object instance interface.
 */

namespace OclLibrary.iface.common
{
    public interface CoreAssociationClass : CoreAssociation, CoreClassifier
    {
        /**
         * @param c 
         * @return 
         */
        CoreAssociationEnd lookupAssociationEnd(CoreClassifier c);
    }
}