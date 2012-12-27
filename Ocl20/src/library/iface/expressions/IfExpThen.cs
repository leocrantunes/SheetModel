/**
 * IfExpThen association proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface IfExpThen {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param thenClause Value of the first association end.
     * @param thenExpression Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(IfExp thenClause, OclExpression thenExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param thenClause Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        IfExp getThenClause(OclExpression thenExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param thenExpression Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getThenExpression(IfExp thenClause);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param thenClause Value of the first association end.
     * @param thenExpression Value of the second association end.
     */
        bool add(IfExp thenClause, OclExpression thenExpression);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param thenClause Value of the first association end.
     * @param thenExpression Value of the second association end.
     */
        bool remove(IfExp thenClause, OclExpression thenExpression);
    }
}
