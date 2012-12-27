/**
 * OclModelElementType class proxy interface.
 */

namespace Ocl20.library.iface.types
{
    public interface OclModelElementTypeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclModelElementType createOclModelElementType();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OclModelElementType createOclModelElementType(string name);
    }
}
