

/**
 * OclType class proxy interface.
 */
namespace Ocl20.library.iface.types
{
    public interface OclTypeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclType createOclType();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OclType createOclType(string name);
    }
}
