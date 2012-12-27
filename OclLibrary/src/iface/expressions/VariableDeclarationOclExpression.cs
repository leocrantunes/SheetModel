/**
 * VariableDeclarationOclExpression association proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface VariableDeclarationOclExpression {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param initializedVariable Value of the first association end.
     * @param initExpression Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(VariableDeclaration initializedVariable, OclExpression initExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param initializedVariable Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        VariableDeclaration getInitializedVariable(OclExpression initExpression);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param initExpression Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getInitExpression(VariableDeclaration initializedVariable);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param initializedVariable Value of the first association end.
     * @param initExpression Value of the second association end.
     */
        bool add(VariableDeclaration initializedVariable, OclExpression initExpression);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param initializedVariable Value of the first association end.
     * @param initExpression Value of the second association end.
     */
        bool remove(VariableDeclaration initializedVariable, OclExpression initExpression);
    }
}
