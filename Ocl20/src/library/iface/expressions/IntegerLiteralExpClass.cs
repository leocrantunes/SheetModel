/**
 * IntegerLiteralExp class proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface IntegerLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        IntegerLiteralExp createIntegerLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @param integerSymbol 
     * @return The created instance object.
     */
        IntegerLiteralExp createIntegerLiteralExp(string name, long integerSymbol);
    }
}
