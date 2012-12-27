/**
 * AssociationClassCallExp class proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface AssociationClassCallExpClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        AssociationClassCallExp createAssociationClassCallExp();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        AssociationClassCallExp createAssociationClassCallExp(string name);
    }
}
