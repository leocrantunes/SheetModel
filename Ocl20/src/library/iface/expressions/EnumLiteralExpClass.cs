/**
 * EnumLiteralExp class proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface EnumLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        EnumLiteralExp createEnumLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        EnumLiteralExp createEnumLiteralExp(string name);
    }
}
