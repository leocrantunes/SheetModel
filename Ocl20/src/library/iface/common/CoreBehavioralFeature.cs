/**
 * CoreBehavioralFeature object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreBehavioralFeature : CoreFeature {
       
        bool getIsQuery();
        void setIsQuery(bool newValue);
        List<Parameter> getParameter();
        void setParameter(List<Parameter> newValue);
    }
}
