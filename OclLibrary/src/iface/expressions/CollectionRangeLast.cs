/**
 * CollectionRangeLast association proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface CollectionRangeLast {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionRange Value of the first association end.
     * @param last Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionRange collectionRange, OclExpression last);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param collectionRange Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CollectionRange getCollectionRange(OclExpression last);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param last Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getLast(CollectionRange collectionRange);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionRange Value of the first association end.
     * @param last Value of the second association end.
     */
        bool add(CollectionRange collectionRange, OclExpression last);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionRange Value of the first association end.
     * @param last Value of the second association end.
     */
        bool remove(CollectionRange collectionRange, OclExpression last);
    }
}
