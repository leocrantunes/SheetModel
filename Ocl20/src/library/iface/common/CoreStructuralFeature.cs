/**
 * CoreStructuralFeature object instance interface.
 */

namespace Ocl20.library.iface.common
{
    public interface CoreStructuralFeature : CoreFeature {
        /**
     * Returns the value of reference featureType.
     * @return Value of reference featureType.
     */
        CoreClassifier getFeatureType();
        /**
     * Sets the value of reference featureType. See {@link #getFeatureType} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setFeatureType(CoreClassifier newValue);
    }
}
