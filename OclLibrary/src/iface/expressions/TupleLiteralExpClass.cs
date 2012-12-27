

/**
 * TupleLiteralExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface TupleLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        TupleLiteralExp createTupleLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        TupleLiteralExp createTupleLiteralExp(string name);
    }
}
