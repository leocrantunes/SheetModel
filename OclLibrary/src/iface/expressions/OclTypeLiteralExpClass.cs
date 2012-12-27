/**
 * OclTypeLiteralExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface OclTypeLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclTypeLiteralExp createOclTypeLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OclTypeLiteralExp createOclTypeLiteralExp(string name);
    }
}
