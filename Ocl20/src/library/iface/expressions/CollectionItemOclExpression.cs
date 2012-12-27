/**
 * CollectionItemOclExpression association proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface CollectionItemOclExpression {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionItem Value of the first association end.
     * @param item Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionItem collectionItem, OclExpression item);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param collectionItem Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CollectionItem getCollectionItem(OclExpression item);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param item Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getItem(CollectionItem collectionItem);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionItem Value of the first association end.
     * @param item Value of the second association end.
     */
        bool add(CollectionItem collectionItem, OclExpression item);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionItem Value of the first association end.
     * @param item Value of the second association end.
     */
        bool remove(CollectionItem collectionItem, OclExpression item);
    }
}
