/**
 * OclInvariantConstraint class proxy interface.
 */
namespace Ocl20.library.iface.constraints
{
    public interface OclInvariantConstraintClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclInvariantConstraint createOclInvariantConstraint();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param source 
     * @param name 
     * @return The created instance object.
     */
        OclInvariantConstraint createOclInvariantConstraint(string source, string name);
    }
}
