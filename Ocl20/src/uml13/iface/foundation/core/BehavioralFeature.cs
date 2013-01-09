/**
 * BehavioralFeature object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface BehavioralFeature : Feature {
        /**
     * Returns the value of attribute isQuery.
     * @return Value of attribute isQuery.
     */
        bool isQuery();
        /**
     * Sets the value of isQuery attribute. See {@link #isQuery} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setQuery(bool newValue);
        /**
     * Returns the value of reference parameter.
     * @return Value of reference parameter.
     */
        List<object> getParameter();
    }
}
