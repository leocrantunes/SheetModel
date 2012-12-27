/**
 * IntegerLiteralExp object instance interface.
 */
namespace Ocl20.library.iface.expressions
{
    public interface IntegerLiteralExp : NumericLiteralExp {
        /**
     * Returns the value of attribute integerSymbol.
     * @return Value of attribute integerSymbol.
     */
        long getIntegerSymbol();
        /**
     * Sets the value of integerSymbol attribute. See {@link #getIntegerSymbol} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setIntegerSymbol(long newValue);
    }
}
