/**
 * CollectionOperation class proxy interface.
 */

namespace OclLibrary.iface.types
{
    public interface CollectionOperationClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        CollectionOperation createCollectionOperation();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        CollectionOperation createCollectionOperation(string name);
    }
}
