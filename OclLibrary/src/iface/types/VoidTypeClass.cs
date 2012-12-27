

/**
 * VoidType class proxy interface.
 */

using OclLibrary.iface.types;

public interface VoidTypeClass{
    /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
    VoidType createVoidType();
    /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
    VoidType createVoidType(string name);
}
