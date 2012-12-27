

/**
 * factoryCollectionType association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.util;

namespace Ocl20.library.iface.types
{
    public interface FactoryCollectionType {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionType Value of the first association end.
     * @param factory Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionType collectionType, AstOclModelElementFactory factory);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param collectionType Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getCollectionType(AstOclModelElementFactory factory);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param factory Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        AstOclModelElementFactory getFactory(CollectionType collectionType);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionType Value of the first association end.
     * @param factory Value of the second association end.
     */
        bool add(CollectionType collectionType, AstOclModelElementFactory factory);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionType Value of the first association end.
     * @param factory Value of the second association end.
     */
        bool remove(CollectionType collectionType, AstOclModelElementFactory factory);
    }
}
