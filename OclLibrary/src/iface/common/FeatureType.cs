/**
 * featureType association proxy interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OclLibrary.iface.common
{
    public interface FeatureType {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param csf Value of the first association end.
     * @param featureType Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreStructuralFeature csf, CoreClassifier featureType);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param csf Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getCsf(CoreClassifier featureType);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param featureType Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getFeatureType(CoreStructuralFeature csf);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param csf Value of the first association end.
     * @param featureType Value of the second association end.
     */
        bool add(CoreStructuralFeature csf, CoreClassifier featureType);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param csf Value of the first association end.
     * @param featureType Value of the second association end.
     */
        bool remove(CoreStructuralFeature csf, CoreClassifier featureType);
    }
}
