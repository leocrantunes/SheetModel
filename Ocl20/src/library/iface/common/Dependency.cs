/**
 * Dependency object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface Dependency : Relationship {

       List<object> getClient();
       void setClient(List<object> newValue);

       List<object> getSupplier();
       void setSupplier(List<object> newValue);
    }
}
