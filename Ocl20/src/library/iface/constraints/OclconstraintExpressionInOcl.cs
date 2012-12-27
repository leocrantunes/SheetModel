/**
 * OCLConstraintExpressionInOCL association proxy interface.
 */

namespace Ocl20.library.iface.constraints
{
    public interface OclconstraintExpressionInOcl {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param expression Value of the first association end.
     * @param constraint Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(ExpressionInOcl expression, OclConstraint constraint);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param expression Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        ExpressionInOcl getExpression(OclConstraint constraint);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param constraint Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclConstraint getConstraint(ExpressionInOcl expression);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param expression Value of the first association end.
     * @param constraint Value of the second association end.
     */
        bool add(ExpressionInOcl expression, OclConstraint constraint);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param expression Value of the first association end.
     * @param constraint Value of the second association end.
     */
        bool remove(ExpressionInOcl expression, OclConstraint constraint);
    }
}
