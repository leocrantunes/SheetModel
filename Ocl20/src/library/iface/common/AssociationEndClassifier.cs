/**
 * AssociationEndClassifier association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface AssociationEndClassifier {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theParticipant Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(expressions.CoreAssociationEnd theAssociationEnd, CoreClassifier theParticipant);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param theAssociationEnd Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getTheAssociationEnd(CoreClassifier theParticipant);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param theParticipant Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getTheParticipant(expressions.CoreAssociationEnd theAssociationEnd);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theParticipant Value of the second association end.
     */
        bool add(expressions.CoreAssociationEnd theAssociationEnd, CoreClassifier theParticipant);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theParticipant Value of the second association end.
     */
        bool remove(expressions.CoreAssociationEnd theAssociationEnd, CoreClassifier theParticipant);
    }
}
