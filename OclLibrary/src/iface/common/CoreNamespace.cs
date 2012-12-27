/**
 * CoreNamespace object instance interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using OclLibrary.environment;
using OclLibrary.iface.environment;
using OclLibrary.impl.environment;

namespace OclLibrary.iface.common
{
    public interface CoreNamespace : CoreModelElement {
        /**
     * @return 
     */
        List<object> getAllAssociations();
        /**
     * @return 
     */
        Environment getEnvironmentWithParents();
        /**
     * @return 
     */
        Environment getEnvironmentWithoutParents();
        /**
     * Returns the value of reference elementsForEnvironment.
     * @return Value of reference elementsForEnvironment.
     */
        List<object> getElementsForEnvironment();
    }
}
