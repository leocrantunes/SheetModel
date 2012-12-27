/**
 * CoreEnumLiteral object instance interface.
 */

namespace Ocl20.library.iface.common
{
    public interface CoreEnumLiteral : CoreModelElement {
        /**
     * Returns the value of reference theEnumeration.
     * @return Value of reference theEnumeration.
     */
        CoreEnumeration getTheEnumeration();
        /**
     * Sets the value of reference theEnumeration. See {@link #getTheEnumeration} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setTheEnumeration(CoreEnumeration newValue);
    }
}
