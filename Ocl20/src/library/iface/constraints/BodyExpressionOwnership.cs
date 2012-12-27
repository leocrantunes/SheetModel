/**
 * bodyExpressionOwnership association proxy interface.
 */

using Ocl20.library.iface.expressions;

namespace Ocl20.library.iface.constraints
{
    public interface BodyExpressionOwnership {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param expressionInOcl Value of the first association end.
     * @param bodyExpression Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(ExpressionInOcl expressionInOcl, CoreAssociationEnd bodyExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param expressionInOcl Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        ExpressionInOcl getExpressionInOcl(CoreAssociationEnd bodyExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param bodyExpression Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationEnd getBodyExpression(ExpressionInOcl expressionInOcl);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param expressionInOcl Value of the first association end.
     * @param bodyExpression Value of the second association end.
     */
        bool add(ExpressionInOcl expressionInOcl, CoreAssociationEnd bodyExpression);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param expressionInOcl Value of the first association end.
     * @param bodyExpression Value of the second association end.
     */
        bool remove(ExpressionInOcl expressionInOcl, CoreAssociationEnd bodyExpression);
    }
}
