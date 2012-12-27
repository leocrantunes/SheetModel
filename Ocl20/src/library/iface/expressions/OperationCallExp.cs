/**
 * OperationCallExp object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.expressions
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
