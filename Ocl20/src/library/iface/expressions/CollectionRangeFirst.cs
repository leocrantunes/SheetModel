

/**
 * CollectionRangeFirst association proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface CollectionRangeFirst {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionRange Value of the first association end.
     * @param first Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionRange collectionRange, OclExpression first);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param collectionRange Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CollectionRange getCollectionRange(OclExpression first);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param first Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getFirst(CollectionRange collectionRange);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionRange Value of the first association end.
     * @param first Value of the second association end.
     */
        bool add(CollectionRange collectionRange, OclExpression first);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionRange Value of the first association end.
     * @param first Value of the second association end.
     */
        bool remove(CollectionRange collectionRange, OclExpression first);
    }
}
