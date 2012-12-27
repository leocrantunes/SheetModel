/**
 * CoreStereotype class proxy interface.
 */

namespace Ocl20.library.iface.common
{
    public interface CoreStereotypeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        CoreStereotype createCoreStereotype();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        CoreStereotype createCoreStereotype(string name);
    }
}
