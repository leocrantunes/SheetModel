/**
 * is context for association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface IsContextFor {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param constraint Value of the first association end.
     * @param contextualClassifier Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclClassifierConstraint constraint, CoreClassifier contextualClassifier);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param constraint Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getConstraint(CoreClassifier contextualClassifier);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param contextualClassifier Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getContextualClassifier(OclClassifierConstraint constraint);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param constraint Value of the first association end.
     * @param contextualClassifier Value of the second association end.
     */
        bool add(OclClassifierConstraint constraint, CoreClassifier contextualClassifier);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param constraint Value of the first association end.
     * @param contextualClassifier Value of the second association end.
     */
        bool remove(OclClassifierConstraint constraint, CoreClassifier contextualClassifier);
    }
}
