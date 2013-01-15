/**
 * CoreBehavioralFeature object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreBehavioralFeature : CoreFeature {
       
        bool isQuery();
        List<object> getParameter();
    }
}
