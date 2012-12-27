/**
 * TupleTypeTuplePartType association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.types
{
    public interface TupleTypeTuplePartType {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param tupleType Value of the first association end.
     * @param tupleParts Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(TupleType tupleType, TuplePartType tupleParts);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param tupleType Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        TupleType getTupleType(TuplePartType tupleParts);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param tupleParts Required value of the second association end.
     * @return Collection of related objects.
     */
        List<object> getTupleParts(TupleType tupleType);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param tupleType Value of the first association end.
     * @param tupleParts Value of the second association end.
     */
        bool add(TupleType tupleType, TuplePartType tupleParts);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param tupleType Value of the first association end.
     * @param tupleParts Value of the second association end.
     */
        bool remove(TupleType tupleType, TuplePartType tupleParts);
    }
}
