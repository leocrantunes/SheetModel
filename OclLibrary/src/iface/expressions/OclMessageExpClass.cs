

/**
 * OclMessageExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface OclMessageExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclMessageExp createOclMessageExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OclMessageExp createOclMessageExp(string name);
    }
}
