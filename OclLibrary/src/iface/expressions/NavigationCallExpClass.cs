/**
 * NavigationCallExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface NavigationCallExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        NavigationCallExp createNavigationCallExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        NavigationCallExp createNavigationCallExp(string name);
    }
}
