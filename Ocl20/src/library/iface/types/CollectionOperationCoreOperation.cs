

/**
 * CollectionOperationCoreOperation association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.types
{
    public interface CollectionOperationCoreOperation {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionOperation Value of the first association end.
     * @param jmiOperation Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionOperation collectionOperation, CoreOperation jmiOperation);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param collectionOperation Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getCollectionOperation(CoreOperation jmiOperation);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param jmiOperation Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreOperation getJmiOperation(CollectionOperation collectionOperation);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionOperation Value of the first association end.
     * @param jmiOperation Value of the second association end.
     */
        bool add(CollectionOperation collectionOperation, CoreOperation jmiOperation);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionOperation Value of the first association end.
     * @param jmiOperation Value of the second association end.
     */
        bool remove(CollectionOperation collectionOperation, CoreOperation jmiOperation);
    }
}
