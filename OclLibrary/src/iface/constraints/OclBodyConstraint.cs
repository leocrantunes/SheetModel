/**
 * OclBodyConstraint object instance interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.constraints
{
    public interface OclBodyConstraint : OclOperationConstraint {
        /**
     * Returns the value of attribute parameterNames.
     * @return Value of attribute parameterNames.
     */
        List<string> getParameterNames();
        /**
     * Sets the value of parameterNames attribute. See {@link #getParameterNames} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setParameterNames(List<string> newValue);
    }
}
