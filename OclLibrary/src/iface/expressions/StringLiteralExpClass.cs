/**
 * StringLiteralExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface StringLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        StringLiteralExp createStringLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @param stringSymbol 
     * @return The created instance object.
     */
        StringLiteralExp createStringLiteralExp(string name, string stringSymbol);
    }
}
