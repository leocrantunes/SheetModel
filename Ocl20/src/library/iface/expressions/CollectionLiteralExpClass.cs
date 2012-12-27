

/**
 * CollectionLiteralExp class proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface CollectionLiteralExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        CollectionLiteralExp createCollectionLiteralExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @param kind 
     * @return The created instance object.
     */
        CollectionLiteralExp createCollectionLiteralExp(string name, CollectionKind kind);
    }
}
