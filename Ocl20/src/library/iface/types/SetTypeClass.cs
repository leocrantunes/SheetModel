/**
 * SetType class proxy interface.
 */

namespace Ocl20.library.iface.types
{
    public interface SetTypeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        SetType createSetType();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        SetType createSetType(string name);
    }
}
