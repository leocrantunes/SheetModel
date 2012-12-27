/**
 * OclDefinedOperation class proxy interface.
 */

namespace OclLibrary.iface.constraints
{
    public interface OclDefinedOperationClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclDefinedOperation createOclDefinedOperation();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OclDefinedOperation createOclDefinedOperation(string name);
    }
}
