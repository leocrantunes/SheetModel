/**
 * OclPostConstraint class proxy interface.
 */

namespace OclLibrary.iface.constraints
{
    public interface OclPostConstraintClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclPostConstraint createOclPostConstraint();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param source 
     * @param name 
     * @return The created instance object.
     */
        OclPostConstraint createOclPostConstraint(string source, string name);
    }
}
