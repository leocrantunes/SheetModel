/**
 * PropertyCallExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface PropertyCallExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        PropertyCallExp createPropertyCallExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        PropertyCallExp createPropertyCallExp(string name);
    }
}
