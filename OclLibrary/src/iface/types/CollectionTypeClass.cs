/**
 * CollectionType class proxy interface.
 */

public interface CollectionTypeClass {
    /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
    CollectionType createCollectionType();
    /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
    CollectionType createCollectionType(string name);
}
