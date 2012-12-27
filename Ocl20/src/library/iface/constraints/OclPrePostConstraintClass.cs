/**
 * OclPrePostConstraint class proxy interface.
 */

namespace Ocl20.library.iface.constraints
{
    public interface OclPrePostConstraintClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclPrePostConstraint createOclPrePostConstraint();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param source 
     * @return The created instance object.
     */
        OclPrePostConstraint createOclPrePostConstraint(string source);
    }
}
