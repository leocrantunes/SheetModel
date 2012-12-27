/**
 * CoreNamespace object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.environment;

namespace Ocl20.library.iface.common
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
