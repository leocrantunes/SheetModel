/**
 * OclDefinedAttribute class proxy interface.
 */

namespace OclLibrary.iface.constraints
{
    public interface OclDefinedAttributeClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclDefinedAttribute createOclDefinedAttribute();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        OclDefinedAttribute createOclDefinedAttribute(string name);
    }
}
