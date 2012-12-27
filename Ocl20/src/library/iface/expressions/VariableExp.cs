/**
 * VariableExp object instance interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface VariableExp : OclExpression {
        /**
     * Returns the value of reference referredVariable.
     * @return Value of reference referredVariable.
     */
        VariableDeclaration getReferredVariable();
        /**
     * Sets the value of reference referredVariable. See {@link #getReferredVariable} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setReferredVariable(VariableDeclaration newValue);
    }
}
