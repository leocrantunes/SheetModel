

/**
 * CollectionLiteralPart class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface CollectionLiteralPartClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        CollectionLiteralPart createCollectionLiteralPart();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        CollectionLiteralPart createCollectionLiteralPart(string name);
    }
}
