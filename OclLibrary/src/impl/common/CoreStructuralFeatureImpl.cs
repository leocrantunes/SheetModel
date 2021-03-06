using OclLibrary.iface.common;

namespace OclLibrary.impl.common
{
    public abstract class CoreStructuralFeatureImpl : CoreFeatureImpl, CoreStructuralFeature {

        /* (non-Javadoc)
	 * @see ocl20.CoreStructuralFeature#getFeatureType()
	 */
        public virtual CoreClassifier getFeatureType() {
            return getSpecificType();
        }

        public abstract void setFeatureType(CoreClassifier newValue);

        protected CoreClassifier getSpecificType() {
            return	null;
        }
    }
}
