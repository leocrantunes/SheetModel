/**
 * IterateExp object instance interface.
 */
namespace Ocl20.library.iface.expressions
{
    public interface IterateExp : LoopExp {
        /**
     * Returns the value of reference result.
     * @return Value of reference result.
     */
        VariableDeclaration getResult();
        /**
     * Sets the value of reference result. See {@link #getResult} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setResult(VariableDeclaration newValue);
    }
}
