using System;
using System.Collections.Generic;

namespace Ocl20.library.iface.constraints
{
    public interface OclActionBodyConstraint : OclOperationConstraint {

        void setAction(Action action);
        Action	getAction();
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
