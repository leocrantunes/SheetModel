/**
 * LoopExp object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.expressions
{
    public interface LoopExp : PropertyCallExp {
        /**
     * Returns the value of reference iterators.
     * @return Value of reference iterators.
     */
        List<VariableDeclaration> getIterators();
        /**
     * Returns the value of reference body.
     * @return Value of reference body.
     */
        OclExpression getBody();
        /**
     * Sets the value of reference body. See {@link #getBody} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setBody(OclExpression newValue);
    }
}
