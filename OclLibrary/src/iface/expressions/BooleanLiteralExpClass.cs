/**
 * boolLiteralExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface boolLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        BooleanLiteralExp createboolLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @param boolSymbol 
     * @return The created instance object.
     */
        BooleanLiteralExp createboolLiteralExp(string name, bool boolSymbol);
    }
}
