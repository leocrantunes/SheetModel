/**
 * OclBodyConstraint class proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.constraints
{
    public interface OclBodyConstraintClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        OclBodyConstraint createOclBodyConstraint();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param source 
     * @param parameterNames 
     * @return The created instance object.
     */
        OclBodyConstraint createOclBodyConstraint(string source, List<object> parameterNames);
    }
}
