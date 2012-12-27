/**
 * CollectionLiteralExpLiteralPart association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.expressions
{
    public interface CollectionLiteralExpLiteralPart {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param literalExp Value of the first association end.
     * @param parts Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CollectionLiteralExp literalExp, CollectionLiteralPart parts);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param literalExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CollectionLiteralExp getLiteralExp(CollectionLiteralPart parts);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param parts Required value of the second association end.
     * @return List of related objects.
     */
        List<object> getParts(CollectionLiteralExp literalExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param literalExp Value of the first association end.
     * @param parts Value of the second association end.
     */
        bool add(CollectionLiteralExp literalExp, CollectionLiteralPart parts);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param literalExp Value of the first association end.
     * @param parts Value of the second association end.
     */
        bool remove(CollectionLiteralExp literalExp, CollectionLiteralPart parts);
    }
}
