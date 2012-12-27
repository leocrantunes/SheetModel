

/**
 * LetExp object instance interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface LetExp : OclExpression {
        /**
     * Returns the value of reference in.
     * @return Value of reference in.
     */
        OclExpression getIn();
        /**
     * Sets the value of reference in. See {@link #getIn} for description on the 
     * reference.
     * @param newValue New value to be set.
     */
        void setIn(OclExpression newValue);
        /**
     * Returns the value of reference variable.
     * @return Value of reference variable.
     */
        VariableDeclaration getVariable();
        /**
     * Sets the value of reference variable. See {@link #getVariable} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setVariable(VariableDeclaration newValue);
    }
}
