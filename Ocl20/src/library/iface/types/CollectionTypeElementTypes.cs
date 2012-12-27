/**
 * CollectionTypeElementTypes association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.types
{
    public interface CollectionTypeElementTypes {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionTypes Value of the first association end.
     * @param elementType Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionType collectionTypes, CoreClassifier elementType);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param collectionTypes Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getCollectionTypes(CoreClassifier elementType);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param elementType Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getElementType(CollectionType collectionTypes);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionTypes Value of the first association end.
     * @param elementType Value of the second association end.
     */
        bool add(CollectionType collectionTypes, CoreClassifier elementType);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionTypes Value of the first association end.
     * @param elementType Value of the second association end.
     */
        bool remove(CollectionType collectionTypes, CoreClassifier elementType);
    }
}
