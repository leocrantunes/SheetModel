/**
 * IfExpElse association proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface IfExpElse {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param elseClause Value of the first association end.
     * @param elseExpression Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(IfExp elseClause, OclExpression elseExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param elseClause Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        IfExp getElseClause(OclExpression elseExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param elseExpression Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getElseExpression(IfExp elseClause);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param elseClause Value of the first association end.
     * @param elseExpression Value of the second association end.
     */
        bool add(IfExp elseClause, OclExpression elseExpression);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param elseClause Value of the first association end.
     * @param elseExpression Value of the second association end.
     */
        bool remove(IfExp elseClause, OclExpression elseExpression);
    }
}
