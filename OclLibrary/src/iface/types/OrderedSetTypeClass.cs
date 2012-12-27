/**
 * OrderedSetType class proxy interface.
 */

namespace OclLibrary.iface.types
{
    public interface OrderedSetTypeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OrderedSetType createOrderedSetType();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OrderedSetType createOrderedSetType(string name);
    }
}
