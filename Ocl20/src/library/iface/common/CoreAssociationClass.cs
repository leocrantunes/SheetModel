/**
 * CoreAssociationClass object instance interface.
 */

namespace Ocl20.library.iface.common
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