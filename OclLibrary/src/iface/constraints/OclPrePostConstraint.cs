/**
 * OclPrePostConstraint object instance interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.constraints
{
    public interface OclPrePostConstraint : OclOperationConstraint {
        /**
     * Returns the value of reference preConditions.
     * @return Value of reference preConditions.
     */
        List<OclPreConstraint> getPreConditions();
        /**
     * Returns the value of reference postConditions.
     * @return Value of reference postConditions.
     */
        List<OclPostConstraint> getPostConditions();
    
        void addPreCondition(OclPreConstraint constraint);
    
        void addPostCondition(OclPostConstraint constraint);
    }
}
