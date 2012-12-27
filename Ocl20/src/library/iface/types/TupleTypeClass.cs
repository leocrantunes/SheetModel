/**
 * TupleType class proxy interface.
 */

namespace Ocl20.library.iface.types
{
    public interface TupleTypeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        TupleType createTupleType();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        TupleType createTupleType(string name);
    }
}
