/**
 * OperationCallExp object instance interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface OperationCallExp : ModelPropertyCallExp {
        /**
     * Returns the value of reference arguments.
     * @return Value of reference arguments.
     */
        List<OclExpression> getArguments();
        /**
     * Returns the value of reference referredOperation.
     * @return Value of reference referredOperation.
     */
        CoreOperation getReferredOperation();
        /**
     * Sets the value of reference referredOperation. See {@link #getReferredOperation} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setReferredOperation(CoreOperation newValue);
    }
}
