/**
 * IfExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface IfExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        IfExp createIfExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        IfExp createIfExp(string name);
    }
}
