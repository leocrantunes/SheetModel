/**
 * TupleLiteralExpTuplePart association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.expressions
{
    public interface TupleLiteralExpTuplePart {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param tupleLiteralExp Value of the first association end.
     * @param tuplePart Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(TupleLiteralExp tupleLiteralExp, VariableDeclaration tuplePart);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param tupleLiteralExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        TupleLiteralExp getTupleLiteralExp(VariableDeclaration tuplePart);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param tuplePart Required value of the second association end.
     * @return Collection of related objects.
     */
        List<object> getTuplePart(TupleLiteralExp tupleLiteralExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param tupleLiteralExp Value of the first association end.
     * @param tuplePart Value of the second association end.
     */
        bool add(TupleLiteralExp tupleLiteralExp, VariableDeclaration tuplePart);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param tupleLiteralExp Value of the first association end.
     * @param tuplePart Value of the second association end.
     */
        bool remove(TupleLiteralExp tupleLiteralExp, VariableDeclaration tuplePart);
    }
}
