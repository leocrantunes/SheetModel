using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public abstract class CoreBehavioralFeatureImpl : CoreFeatureImpl, CoreBehavioralFeature
    {
        public abstract bool isQuery();
    }
}
