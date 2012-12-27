/**
 * OclOperationConstraint object instance interface.
 */

using System;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface OclOperationConstraint : OclConstraint, ICloneable {
        /**
     * Returns the value of reference contextualOperation.
     * @return Value of reference contextualOperation.
     */
        CoreOperation getContextualOperation();
        /**
     * Sets the value of reference contextualOperation. See {@link #getContextualOperation} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setContextualOperation(CoreOperation newValue);
    }
}
