using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public abstract class CoreStructuralFeatureImpl : CoreFeatureImpl, CoreStructuralFeature {

        /* (non-Javadoc)
	 * @see ocl20.CoreStructuralFeature#getFeatureType()
	 */
        public virtual CoreClassifier getFeatureType() {
            return getSpecificType();
        }

        public abstract void setFeatureType(CoreClassifier newValue);

        public virtual CoreClassifier getSpecificType() {
            return	null;
        }
    }
}
