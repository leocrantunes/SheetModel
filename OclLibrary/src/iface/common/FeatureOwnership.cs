/**
 * featureOwnership association proxy interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OclLibrary.iface.common
{
    public interface FeatureOwnership {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param featureOwner Value of the first association end.
     * @param classifierFeatures Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreClassifier featureOwner, CoreFeature classifierFeatures);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param featureOwner Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getFeatureOwner(CoreFeature classifierFeatures);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param classifierFeatures Required value of the second association end.
     * @return Collection of related objects.
     */
        List<object> getClassifierFeatures(CoreClassifier featureOwner);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param featureOwner Value of the first association end.
     * @param classifierFeatures Value of the second association end.
     */
        bool add(CoreClassifier featureOwner, CoreFeature classifierFeatures);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param featureOwner Value of the first association end.
     * @param classifierFeatures Value of the second association end.
     */
        bool remove(CoreClassifier featureOwner, CoreFeature classifierFeatures);
    }
}
