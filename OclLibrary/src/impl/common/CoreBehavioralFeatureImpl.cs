using OclLibrary.iface.common;

namespace OclLibrary.impl.common
{
    public abstract class CoreBehavioralFeatureImpl : CoreFeatureImpl, CoreBehavioralFeature
    {
        public abstract bool isQuery();
    }
}
