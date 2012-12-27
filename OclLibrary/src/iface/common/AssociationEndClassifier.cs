/**
 * AssociationEndClassifier association proxy interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using OclLibrary.iface.common;
using CoreAssociationEnd = OclLibrary.iface.expressions.CoreAssociationEnd;

public interface AssociationEndClassifier {
    /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theParticipant Value of the second association end.
     * @return Returns true if the queried link exists.
     */
    bool exists(CoreAssociationEnd theAssociationEnd, CoreClassifier theParticipant);
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
    CoreClassifier getTheParticipant(CoreAssociationEnd theAssociationEnd);
    /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theParticipant Value of the second association end.
     */
    bool add(CoreAssociationEnd theAssociationEnd, CoreClassifier theParticipant);
    /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theParticipant Value of the second association end.
     */
    bool remove(CoreAssociationEnd theAssociationEnd, CoreClassifier theParticipant);
}
