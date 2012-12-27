/**
 * LoopExpOclExpression association proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface LoopExpOclExpression {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param loopExp Value of the first association end.
     * @param body Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(LoopExp loopExp, OclExpression body);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param loopExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        LoopExp getLoopExp(OclExpression body);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param body Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getBody(LoopExp loopExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param loopExp Value of the first association end.
     * @param body Value of the second association end.
     */
        bool add(LoopExp loopExp, OclExpression body);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param loopExp Value of the first association end.
     * @param body Value of the second association end.
     */
        bool remove(LoopExp loopExp, OclExpression body);
    }
}
