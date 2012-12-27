/**
 * OclOperationConstraint object instance interface.
 */

using System;
using OclLibrary.iface.common;

namespace OclLibrary.iface.constraints
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
