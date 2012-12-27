/**
 * VariableExp class proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface VariableExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        VariableExp createVariableExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        VariableExp createVariableExp(string name);
    }
}
