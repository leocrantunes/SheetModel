/**
 * NumericLiteralExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface NumericLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        NumericLiteralExp createNumericLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        NumericLiteralExp createNumericLiteralExp(string name);
    }
}
