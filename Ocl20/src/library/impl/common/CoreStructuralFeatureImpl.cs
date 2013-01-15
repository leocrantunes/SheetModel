using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreStructuralFeatureImpl : CoreFeatureImpl, CoreStructuralFeature
    {
        private CoreClassifier featureType;

        public CoreStructuralFeatureImpl()
        {
            featureType = null;
        }

        public virtual CoreClassifier getFeatureType()
        {
            return featureType;
        }

        public virtual void setFeatureType(CoreClassifier newValue)
        {
            featureType = newValue;
        }

        public virtual CoreClassifier getSpecificType()
        {
            return getFeatureType();
        }
    }
}
