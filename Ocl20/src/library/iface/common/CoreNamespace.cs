/**
 * CoreNamespace object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.environment;

namespace Ocl20.library.iface.common
{
    public interface CoreNamespace : CoreModelElement {

        List<object> getAllAssociations();
        Environment getEnvironmentWithParents();
        Environment getEnvironmentWithoutParents();
        List<object> getElementsForEnvironment();
    }
}
