/**
 * CollectionOperation object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.types
{
    public interface CollectionOperation : CoreOperation {
        /**
     * Returns the value of reference jmiOperation.
     * @return Value of reference jmiOperation.
     */
        CoreOperation getJmiOperation();
        /**
     * Sets the value of reference jmiOperation. See {@link #getJmiOperation} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setJmiOperation(CoreOperation newValue);
    }
}
