/**
 * CoreFeature object instance interface.
 */

namespace OclLibrary.iface.common
{
    public interface CoreFeature : CoreModelElement {
        /**
     * @return 
     */
        bool isInstanceScope();
        /**
     * @return 
     */
        bool isOclDefined();
        /**
     * Returns the value of reference featureOwner.
     * @return Value of reference featureOwner.
     */
        CoreClassifier getFeatureOwner();
        /**
     * Sets the value of reference featureOwner. See {@link #getFeatureOwner} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setFeatureOwner(CoreClassifier newValue);
    }
}
