/**
 * CollectionLiteralPartCoreClassifier association proxy interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface CollectionLiteralPartCoreClassifier {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param collectionLiteralPart Value of the first association end.
     * @param type Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionLiteralPart collectionLiteralPart, CoreClassifier type);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param collectionLiteralPart Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getCollectionLiteralPart(CoreClassifier type);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param type Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getType(CollectionLiteralPart collectionLiteralPart);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param collectionLiteralPart Value of the first association end.
     * @param type Value of the second association end.
     */
        bool add(CollectionLiteralPart collectionLiteralPart, CoreClassifier type);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param collectionLiteralPart Value of the first association end.
     * @param type Value of the second association end.
     */
        bool remove(CollectionLiteralPart collectionLiteralPart, CoreClassifier type);
    }
}
