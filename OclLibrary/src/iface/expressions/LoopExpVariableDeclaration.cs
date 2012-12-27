/**
 * LoopExpVariableDeclaration association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.expressions
{
    public interface LoopExpVariableDeclaration {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param loopExp Value of the first association end.
     * @param iterators Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(LoopExp loopExp, VariableDeclaration iterators);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param loopExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        LoopExp getLoopExp(VariableDeclaration iterators);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param iterators Required value of the second association end.
     * @return Collection of related objects.
     */
        List<object> getIterators(LoopExp loopExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param loopExp Value of the first association end.
     * @param iterators Value of the second association end.
     */
        bool add(LoopExp loopExp, VariableDeclaration iterators);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param loopExp Value of the first association end.
     * @param iterators Value of the second association end.
     */
        bool remove(LoopExp loopExp, VariableDeclaration iterators);
    }
}
